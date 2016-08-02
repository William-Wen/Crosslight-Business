using System.ComponentModel;
using Business.ViewModels;
using CoreGraphics;
using Intersoft.Crosslight;
using Intersoft.Crosslight.iOS;
using UIKit;

namespace Business.iOS
{
    /// <summary>
    ///     Represents ViewController for Inventory list screen.
    ///     This class is decorated with the ImportBindingAttribute that indicates
    ///     the binding provider to be used with this ViewController.
    ///     The RegisterNavigationAttribute indicates that this ViewController will only be shown on iPhones.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UITableViewController{Business.ViewModels.ItemListViewModel}" />
    [ImportBinding(typeof(ItemListBindingProvider))]
    [RegisterNavigation(DeviceKind.Phone)]
    public partial class ItemListViewController : UITableViewController<ItemListViewModel>
    {
        #region Properties

        private UIBarButtonItem AddButton { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="T:Intersoft.Crosslight.iOS.UITableViewController`1" /> allow
        ///     searching.
        /// </summary>
        /// <value>
        ///     <c>true</c> if allow searching; otherwise, <c>false</c>.
        /// </value>
        public override bool AllowSearching
        {
            get { return true; }
        }

        /// <summary>
        ///     Gets the cell image settings.
        /// </summary>
        /// <value>
        ///     The cell image settings.
        /// </value>
        public override ImageSettings CellImageSettings
        {
            get
            {
                return new ImageSettings
                {
                    ImageSize = new CGSize(36, 36)
                };
            }
        }

        /// <summary>
        ///     Gets the cell style.
        /// </summary>
        /// <value>
        ///     The cell style.
        /// </value>
        public override TableViewCellStyle CellStyle
        {
            get { return TableViewCellStyle.Subtitle; }
        }

        /// <summary>
        ///     Gets the editing options.
        /// </summary>
        /// <value>
        ///     The editing options.
        /// </value>
        public override EditingOptions EditingOptions
        {
            get { return EditingOptions.AllowEditing | EditingOptions.AllowMultipleSelection; }
        }

        private UIBarButtonItem[] EditToolBarItems { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="T:Intersoft.Crosslight.iOS.UITableViewController`1" /> hide
        ///     search bar initially.
        /// </summary>
        /// <value>
        ///     <c>true</c> if hide search bar initially; otherwise, <c>false</c>.
        /// </value>
        public override bool HideSearchBarInitially
        {
            get { return true; }
        }

        /// <summary>
        ///     Gets the image loader settings.
        /// </summary>
        /// <value>
        ///     The image loader settings.
        /// </value>
        public override BasicImageLoaderSettings ImageLoaderSettings
        {
            get
            {
                return new BasicImageLoaderSettings
                {
                    AnimateOnLoad = true,
                    CacheExpirationPolicy = CacheExpirationPolicy.AutoDetect
                };
            }
        }

        /// <summary>
        ///     Gets the interaction mode.
        /// </summary>
        /// <value>
        ///     The interaction mode.
        /// </value>
        public override TableViewInteraction InteractionMode
        {
            get { return TableViewInteraction.Navigation; }
        }

        private UIBarButtonItem[] NormalToolbarItems { get; set; }

        /// <summary>
        ///     Gets the search scopes.
        /// </summary>
        /// <value>
        ///     The search scopes.
        /// </value>
        public override string[] SearchScopes
        {
            get { return new[] { "Name", "Location" }; }
        }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="T:Intersoft.Crosslight.iOS.UITableViewController`1" /> show
        ///     group header.
        /// </summary>
        /// <value>
        ///     <c>true</c> if show group header; otherwise, <c>false</c>.
        /// </value>
        public override bool ShowGroupHeader
        {
            get { return true; }
        }

        #endregion

        #region Methods

        private UIView CreateEditStatusView()
        {
            UIView statusView = new UIView();
            UILabel selectionLabel = new UILabel();

            selectionLabel.Frame = new CGRect(0, 0, 200, 44);
            selectionLabel.Font = selectionLabel.Font.WithSize(13f);
            selectionLabel.TextAlignment = UITextAlignment.Center;

            statusView.Frame = new CGRect(0, 0, 200, 44);
            statusView.AddSubview(selectionLabel);

            this.RegisterViewIdentifier("SelectionLabel", selectionLabel);

            return statusView;
        }

        private UIView CreateStatusView()
        {
            UIView statusView = new UIView();
            UILabel updatedLabel = new UILabel();
            UILabel countLabel = new UILabel();

            updatedLabel.Frame = new CGRect(0, 4, 200, 18);
            updatedLabel.Font = updatedLabel.Font.WithSize(12f);
            updatedLabel.TextAlignment = UITextAlignment.Center;

            countLabel.Frame = new CGRect(0, 20, 200, 18);
            countLabel.Font = updatedLabel.Font.WithSize(12f);
            countLabel.TextColor = UIColor.Gray;
            countLabel.TextAlignment = UITextAlignment.Center;

            statusView.Frame = new CGRect(0, 0, 200, 44);
            statusView.AddSubview(updatedLabel);
            statusView.AddSubview(countLabel);

            this.RegisterViewIdentifier("UpdatedLabel", updatedLabel);
            this.RegisterViewIdentifier("CountLabel", countLabel);

            return statusView;
        }

        /// <summary>
        ///     Initializes the view.
        /// </summary>
        protected override void InitializeView()
        {
            base.InitializeView();

            UIBarButtonItem addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add);

            this.NavigationItem.Title = "My Inventory";
            this.NavigationItem.SetRightBarButtonItem(this.EditButtonItem, false);

            // Configure ToolBar
            UIBarButtonItem deleteButton = new UIBarButtonItem(UIBarButtonSystemItem.Trash);
            UIBarButtonItem markSoldButton = new UIBarButtonItem(UIBarButtonSystemItem.Action);
            UIBarButtonItem flexibleWidth = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            UIBarButtonItem flexibleWidth2 = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            UIBarButtonItem normalStatusView = new UIBarButtonItem(this.CreateStatusView());
            UIBarButtonItem editStatusView = new UIBarButtonItem(this.CreateEditStatusView());

            this.EditToolBarItems = new[] { flexibleWidth, editStatusView, markSoldButton, deleteButton };
            this.NormalToolbarItems = new[] { flexibleWidth, normalStatusView, flexibleWidth2, addButton };

            this.SetToolbarHidden(false, false);
            this.SetToolbarItems(this.NormalToolbarItems, false);

            // Configure Footer
            UILabel totalCountLabel = new UILabel(new CGRect(0, 0, this.TableView.Bounds.Width, 44));
            totalCountLabel.TextAlignment = UITextAlignment.Center;
            totalCountLabel.TextColor = UIColor.Gray;
            this.TableView.TableFooterView = totalCountLabel;

            // Register Views
            this.RegisterViewIdentifier("AddButton", addButton);
            this.RegisterViewIdentifier("EditButton", this.EditButtonItem);
            this.RegisterViewIdentifier("DeleteButton", deleteButton);
            this.RegisterViewIdentifier("MarkSoldButton", markSoldButton);
            this.RegisterViewIdentifier("FooterLabel", totalCountLabel);

            this.AddButton = addButton;
        }

        /// <summary>
        ///     Called when the properties of the associated ViewModel has changed.
        /// </summary>
        /// <param name="e">The event argument.</param>
        protected override void OnViewModelPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnViewModelPropertyChanged(e);

            if (e.PropertyName == "IsEditing")
            {
                if (this.ViewModel.IsEditing)
                    this.SetToolbarItems(this.EditToolBarItems, true);
                else
                    this.SetToolbarItems(this.NormalToolbarItems, true);
            }
        }

        #endregion
    }
}