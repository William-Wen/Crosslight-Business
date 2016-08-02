using System;
using System.ComponentModel;
using System.Linq;
using Business.Models;
using Intersoft.AppFramework.Identity;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Data.EntityModel;
using Intersoft.Crosslight.Forms;
using Intersoft.Crosslight.Input;
using Intersoft.Crosslight.ViewModels;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for the Profile screen.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.ViewModels.EditorViewModelBase{Intersoft.AppFramework.Identity.User}" />
    public class UserProfileViewModel : EditorViewModelBase<User>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserProfileViewModel" /> class.
        /// </summary>
        public UserProfileViewModel()
        {
            this.Title = "Edit Profile";
            this.Item = this.GetService<IUserService>().GetCurrentUser();
            this.FinishImagePickerCommand = new DelegateCommand(ExecuteFinishImagePickerCommand);
            this.LoadUserFailedErrorMessage = "Unable to edit user profile because the server is currently unreachable.";

            if (this.Item != null)
            {
                this.EntityContainer = new EntityContainer();
                this.EntityContainer.AttachEntity(this.Item);

                this.Item.UserDetail.ErrorsChanged += OnUserDetailErrorsChanged;

                this.Item.BeginEdit();
                this.Item.UserDetail.BeginEdit();
            }
        }

        #endregion

        #region Properties

        public DelegateCommand ActivateImagePickerCommand { get; set; }
        private EntityContainer EntityContainer { get; set; }
        public DelegateCommand FinishImagePickerCommand { get; set; }

        /// <summary>
        ///     Gets the type of the form metadata associated to the editor.
        /// </summary>
        /// <value>
        ///     The type of the form metadata.
        /// </value>
        public override Type FormMetadataType
        {
            get { return typeof(UserProfileFormMetadata); }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        ///     true
        /// </value>
        /// <c>false</c>
        public override bool HasErrors
        {
            get { return base.HasErrors || this.Item.UserDetail.HasErrors; }
        }

        public bool IsWebApiAccount
        {
            get
            {
                IAccountService accountService = this.GetService<IAccountService>();
                return accountService.AccountServiceId == accountService.GetAccount().ServiceId;
            }
        }

        public string LoadUserFailedErrorMessage { get; set; }
        public NavigationViewModel ParentViewModel { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Dispose this instance.
        /// </summary>
        /// <param name="isDisposing">If set to <c>true</c> is disposing.</param>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (this.Item != null)
            {
                this.Item.UserDetail.ErrorsChanged -= OnUserDetailErrorsChanged;
                this.EntityContainer.RemoveEntity(this.Item);
                this.EntityContainer.RemoveEntity(this.Item.UserDetail);
                this.EntityContainer = null;
            }
        }

        /// <summary>
        ///     Executes the cancel command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        protected override void ExecuteCancel(object parameter)
        {
            if (this.Item != null)
            {
                this.Item.CancelEdit();
                this.Item.UserDetail.CancelEdit();
            }

            base.ExecuteCancel(parameter);
        }

        /// <summary>
        ///     Executes the finish image picker command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void ExecuteFinishImagePickerCommand(object parameter)
        {
            ImagePickerResultParameter resultParameter = parameter as ImagePickerResultParameter;
            if (resultParameter != null)
            {
                IResourceCacheSession cacheSession = this.GetService<IImageLoaderService>().GetImageLoaderCacheSession();
                IResourceCacheService cacheService = this.GetService<IResourceCacheService>();
                bool raisePropertyChanged = false;

                if (resultParameter.Result != null)
                {
                    this.Item.UserDetail.ImageUrl = Guid.NewGuid().ToString("N") + ".jpg";
                    this.Item.UserDetail.ImageData = resultParameter.Result.ThumbnailImageData;

                    // add to cache service to avoid losing the image changes in case the upload/save failed (due to connection etc)
                    if (cacheService != null && cacheSession != null)
                        cacheService.AddToCache(cacheSession, this.Item.UserDetail.ResolvedImageUrl, this.Item.UserDetail.ImageData, ResourceCacheMode.DiskAndMemory);

                    raisePropertyChanged = true;
                }
                else
                {
                    if (resultParameter.SelectedCommandId == "Delete Photo")
                    {
                        // user tapped the Delete Photo option
                        this.Item.UserDetail.ImageUrl = null;

                        if (cacheService != null && cacheSession != null)
                            cacheService.RemoveFromCache(cacheSession, this.Item.UserDetail.ResolvedImageUrl);

                        raisePropertyChanged = true;
                    }
                }

                if (raisePropertyChanged)
                {
                    // notify other views that consume these properties, so the image changes are reflected automatically
                    this.Item.RaisePropertyChanged("ResolvedImageUrl");
                }
            }
        }

        /// <summary>
        ///     Executes the save command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        protected override async void ExecuteSave(object parameter)
        {
            if (this.Item == null)
            {
                this.NavigationService.Close();
                return;
            }

            this.Validate();

            // submit registration data to server
            if (!this.HasErrors)
            {
                IUserService service = this.GetService<IUserService>();

                try
                {
                    this.ActivityPresenter.Show(ActivityStyle.LargeIndicator);

                    // update user and notify
                    this.Item.EndEdit();
                    this.Item.UserDetail.EndEdit();

                    User user = await service.UpdateUserAsync(this.Item);
                    this.ParentViewModel.User = user;

                    // close this view
                    this.NavigationService.Close(new NavigationResult(NavigationResultAction.Done));
                }
                catch (Exception exception)
                {
                    this.MessagePresenter.Show(exception.GetExceptionMessage(), "Update Failed");
                }
                finally
                {
                    this.ActivityPresenter.Hide();
                }
            }
            else
                this.MessagePresenter.Show(this.ErrorMessage);
        }

        /// <summary>
        ///     Called when this instance is navigated.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        public override void Navigated(NavigatedParameter parameter)
        {
            base.Navigated(parameter);
            this.ParentViewModel = parameter.Sender as NavigationViewModel;
        }

        /// <summary>
        ///     Called when user detail errors changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DataErrorsChangedEventArgs" /> instance containing the event data.</param>
        private void OnUserDetailErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.ErrorMessage) && this.Item.UserDetail.HasErrors)
                this.ErrorMessage = this.Item.UserDetail.GetErrors(null).OfType<ValidationResult>().First().ErrorMessage;
        }

        /// <summary>
        ///     Validate this editor instance.
        /// </summary>
        public override void Validate()
        {
            base.Validate();

            // include validation for user detail
            this.Item.UserDetail.Validate();
        }

        #endregion
    }
}