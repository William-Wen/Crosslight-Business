using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Business.DomainModels.Inventory;
using Intersoft.AppFramework;
using Intersoft.AppFramework.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Forms;
using Intersoft.Crosslight.Input;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for the Add/Edit item screen.
    /// </summary>
    /// <seealso
    ///     cref="Intersoft.AppFramework.ViewModels.DataEditorViewModelBase{Business.DomainModels.Inventory.Item, Business.DomainModels.Inventory.IItemRepository}" />
    public class ItemEditorViewModel : DataEditorViewModelBase<Item, IItemRepository>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemEditorViewModel" /> class.
        /// </summary>
        public ItemEditorViewModel()
        {
            this.Title = "Edit Item";

            this.ActivateImagePickerCommand = new DelegateCommand(ExecuteActivateImagePicker);
            this.FinishImagePickerCommand = new DelegateCommand(ExecuteFinishImagePickerCommand);
            this.ViewLargeImageCommand = new DelegateCommand(ExecuteViewLargeImage, CanExecuteViewLargeImage);
        }

        #endregion

        #region Properties

        public DelegateCommand ActivateImagePickerCommand { get; set; }

        private ICategoryRepository CategoryRepository
        {
            get
            {
                if (Container.Current.CanResolve<ICategoryRepository>())
                    return Container.Current.Resolve<ICategoryRepository>();
                return new CategoryRepository(null); // for designer support
            }
        }

        public DelegateCommand FinishImagePickerCommand { get; set; }

        /// <summary>
        ///     Gets the type of the form metadata associated to the editor.
        /// </summary>
        /// <value>
        ///     The type of the form metadata.
        /// </value>
        public override Type FormMetadataType
        {
            get { return typeof(ItemFormMetadata); }
        }

        public DelegateCommand ViewLargeImageCommand { get; set; }

        #endregion

        #region Methods

        private bool CanExecuteViewLargeImage(object parameter)
        {
            if (this.IsNewItem || string.IsNullOrEmpty(this.Item.ResolvedThumbnailImage))
                return false;

            return true;
        }

        private void ExecuteActivateImagePicker(object parameter)
        {
            ImagePickerActivateParameter activateParameter = parameter as ImagePickerActivateParameter;
            if (activateParameter != null)
            {
                activateParameter.CustomCommands = new Dictionary<string, ICommand>();
                activateParameter.CustomCommands.Add("View Larger", this.ViewLargeImageCommand);
            }
        }

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
                    this.Item.Image = Guid.NewGuid().ToString("N") + ".jpg";
                    this.Item.ThumbnailImage = resultParameter.Result.ThumbnailImageData;
                    this.Item.LargeImage = resultParameter.Result.ImageData;

                    // add to cache service to avoid losing the image changes in case the upload/save failed (due to connection etc)
                    if (cacheService != null && cacheSession != null)
                    {
                        cacheService.AddToCache(cacheSession, this.Item.ResolvedThumbnailImage, this.Item.ThumbnailImage, ResourceCacheMode.DiskAndMemory);
                        cacheService.AddToCache(cacheSession, this.Item.ResolvedLargeImage, this.Item.LargeImage, ResourceCacheMode.DiskAndMemory);
                    }

                    raisePropertyChanged = true;
                }
                else
                {
                    if (resultParameter.SelectedCommandId == "Delete Photo")
                    {
                        // user tapped the Delete Photo option
                        this.Item.Image = null;

                        if (cacheService != null && cacheSession != null)
                        {
                            cacheService.RemoveFromCache(cacheSession, this.Item.ResolvedThumbnailImage);
                            cacheService.RemoveFromCache(cacheSession, this.Item.ResolvedLargeImage);
                        }

                        raisePropertyChanged = true;
                    }
                }

                if (raisePropertyChanged)
                {
                    // notify other views that consume these properties, so the image changes are reflected automatically
                    this.Item.RaisePropertyChanged("ResolvedThumbnailImage");
                    this.Item.RaisePropertyChanged("ResolvedLargeImage");
                }
            }
        }

        private void ExecuteViewLargeImage(object parameter)
        {
            this.NavigationService.Navigate(new NavigationTarget(typeof(ItemDetailViewModel), "PhotoDetail", new NavigationParameter(this.Item)));
        }

        /// <summary>
        ///     Initializes the new item.
        /// </summary>
        /// <param name="item">The item.</param>
        protected override void InitializeNewItem(Item item)
        {
            base.InitializeNewItem(item);

            this.Item = this.Repository.Create();
            this.Item.PurchaseDate = DateTime.Today;
            this.Item.Qty = 1;
            this.Item.Category = this.CategoryRepository.GetAll().OrderBy(o => o.Name).FirstOrDefault();

            this.Title = "Add New Item";
        }

        /// <summary>
        ///     Called when cancel editing an item.
        /// </summary>
        /// <param name="item">The item.</param>
        protected override void OnItemCancelEdit(Item item)
        {
            base.OnItemCancelEdit(item);

            // if users have select a photo, clear it when changes are cancelled.
            item.ThumbnailImage = null;
            item.LargeImage = null;
        }

        #endregion
    }
}