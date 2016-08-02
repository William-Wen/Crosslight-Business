using System;
using Business.Models;
using Intersoft.AppFramework;
using Intersoft.Crosslight.Data.EntityModel;
using Intersoft.Crosslight.ViewModels;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for the Settings screen.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.ViewModels.EditorViewModelBase{Business.Models.UserSettings}" />
    public class UserSettingsViewModel : EditorViewModelBase<UserSettings>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserSettingsViewModel" /> class.
        /// </summary>
        public UserSettingsViewModel()
        {
            this.Title = "Edit Settings";
            this.Item = Container.Current.Resolve<UserSettings>();
            this.EntityContainer = new EntityContainer();
            this.EntityContainer.AttachEntity(this.Item);

            this.Item.BeginEdit();
        }

        #endregion

        #region Properties

        private EntityContainer EntityContainer { get; set; }

        /// <summary>
        ///     Gets the type of the form metadata associated to the editor.
        /// </summary>
        /// <value>
        ///     The type of the form metadata.
        /// </value>
        public override Type FormMetadataType
        {
            get { return typeof(UserSettingsFormMetadata); }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Dispose this instance.
        /// </summary>
        /// <param name="isDisposing">If set to <c>true</c> is disposing.</param>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                this.EntityContainer.RemoveEntity(this.Item);
                this.EntityContainer = null;
            }
        }

        /// <summary>
        ///     Executes the cancel command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        protected override void ExecuteCancel(object parameter)
        {
            this.Item.CancelEdit();

            base.ExecuteCancel(parameter);
        }

        /// <summary>
        ///     Executes the save command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        protected override async void ExecuteSave(object parameter)
        {
            this.Validate();

            if (!this.HasErrors)
            {
                this.Item.EndEdit();
                await this.GetService<IUserSettingsService>().Save(this.Item);
                this.NavigationService.Close();
            }
            else
                this.MessagePresenter.Show(this.ErrorMessage);
        }

        #endregion
    }
}