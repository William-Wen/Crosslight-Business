using System;
using System.ComponentModel;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.iOS;
using UIKit;

namespace Business.iOS
{
    /// <summary>
    ///     Represents ViewController for the item collection view on iPad.
    ///     This class is decorated with the StoryboardAttribute that allows for VC initialization via universal Storyboard.
    ///     This class is decorated with the ImportBindingAttribute that indicates
    ///     the binding provider to be used with this ViewController.
    ///     The RegisterNavigationAttribute indicates that this ViewController will only be shown on iPads.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UICollectionViewController{Business.ViewModels.ItemListViewModel}" />
    [Storyboard("MainStoryboard")]
    [ImportBinding(typeof(ItemListBindingProvider))]
    [RegisterNavigation(DeviceKind.Tablet)]
    public partial class ItemCollectionViewController : UICollectionViewController<ItemListViewModel>
    {
        #region Constructors

        public ItemCollectionViewController(IntPtr intPtr)
            : base(intPtr)
        {
        }

        #endregion

        #region Properties

        private UIBarButtonItem AddButton { get; set; }

        public override UIViewTemplate CellTemplate
        {
            get { return new UIViewTemplate(ItemCollectionViewCell.Nib); }
        }

        private UIBarButtonItem DeleteButton { get; set; }

        public override EditingOptions EditingOptions
        {
            get { return EditingOptions.AllowEditing | EditingOptions.AllowMultipleSelection; }
        }

        public override bool EnterEditModeOnLongPress
        {
            get { return true; }
        }

        public override CollectionViewInteraction InteractionMode
        {
            get { return CollectionViewInteraction.Navigation; }
        }

        private UIBarButtonItem MarkSoldButton { get; set; }

        public override UIViewTemplate SectionHeaderTemplate
        {
            get { return new UIViewTemplate(ItemCollectionViewHeader.Nib); }
        }

        public override bool ShowSectionHeader
        {
            get { return true; }
        }

        #endregion

        #region Methods

        protected override void InitializeView()
        {
            base.InitializeView();

            this.AddButton = new UIBarButtonItem(UIBarButtonSystemItem.Add);

            this.NavigationItem.Title = "My Inventory";
            this.NavigationItem.SetRightBarButtonItem(this.AddButton, false);

            this.DeleteButton = new UIBarButtonItem(UIBarButtonSystemItem.Trash);
            this.MarkSoldButton = new UIBarButtonItem(UIBarButtonSystemItem.Action);

            // Register Views
            this.RegisterViewIdentifier("TableView", this.CollectionView);

            this.RegisterViewIdentifier("AddButton", this.AddButton);
            this.RegisterViewIdentifier("EditButton", this.EditButtonItem);
            this.RegisterViewIdentifier("DeleteButton", this.DeleteButton);
            this.RegisterViewIdentifier("MarkSoldButton", this.MarkSoldButton);
        }

        protected override void OnViewModelPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsEditing")
            {
                if (this.ViewModel.IsEditing)
                    this.NavigationItem.SetRightBarButtonItems(new[] { this.EditButtonItem, this.DeleteButton, this.MarkSoldButton }, true);
                else
                    this.NavigationItem.SetRightBarButtonItems(new[] { this.AddButton }, true);
            }
        }

        #endregion
    }
}