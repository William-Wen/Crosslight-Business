using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.iOS;
using UIKit;

namespace Business.iOS
{
    /// <summary>
    ///     Represents ViewController for navigation menu inside the navigation drawer.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UITableViewController{Business.ViewModels.NavigationViewModel}" />
    [ImportBinding(typeof(NavigationBindingProvider))]
    [RegisterNavigation(IsRootView = true)]
    public class NavigationViewController : UITableViewController<NavigationViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavigationViewController" /> class.
        /// </summary>
        public NavigationViewController()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavigationViewController" /> class.
        /// </summary>
        /// <param name="viewModel">View model.</param>
        public NavigationViewController(NavigationViewModel viewModel)
            : base(viewModel)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether this <see cref="T:Intersoft.Crosslight.iOS.UITableViewController`1" /> deselect
        ///     row on navigate.
        /// </summary>
        /// <value>
        ///     <c>true</c> if deselect row on navigate; otherwise, <c>false</c>.
        /// </value>
        public override bool DeselectRowOnNavigate
        {
            get { return true; }
        }

        /// <summary>
        ///     Gets the footer view template.
        /// </summary>
        /// <value>
        ///     The footer view template.
        /// </value>
        public override UIViewTemplate FooterViewTemplate
        {
            get { return new UIViewTemplate(DrawerFooterTableView.Nib); }
        }

        /// <summary>
        ///     Gets the header view template.
        /// </summary>
        /// <value>
        ///     The header view template.
        /// </value>
        public override UIViewTemplate HeaderViewTemplate
        {
            get { return new UIViewTemplate(DrawerHeaderTableView.Nib); }
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

        /// <summary>
        ///     Gets a value indicating whether this <see cref="T:Intersoft.Crosslight.iOS.UITableViewController`1" /> show
        ///     group header.
        /// </summary>
        /// <value>
        ///     <c>true</c> if show group header; otherwise, <c>false</c>.
        /// </value>
        public override bool ShowGroupHeader
        {
            get { return false; }
        }

        /// <summary>
        ///     Gets the table view style.
        /// </summary>
        /// <value>
        ///     The table view style.
        /// </value>
        public override UITableViewStyle TableViewStyle
        {
            get { return UITableViewStyle.Grouped; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes the view.
        /// </summary>
        protected override void InitializeView()
        {
            base.InitializeView();

            // set TableView background
            this.Appearance.BackgroundImage = "drawer_bg.jpg";
            this.Appearance.BackgroundBlurEnabled = true;

            // set TableCell appearance
            this.Appearance.CellBackgroundColor = UIColor.Clear;
            this.Appearance.CellSelectedBackgroundColor = UIColor.White.ColorWithAlpha(0.3f);
            this.Appearance.CellBackgroundColor = UIColor.White.ColorWithAlpha(0.1f);
            this.Appearance.ShowSeparator = true;

            // set navigation title
            this.NavigationItem.Title = "Crosslight App";
        }

        /// <summary>
        ///     Called when the view is initialized.
        /// </summary>
        protected override void OnViewInitialized()
        {
            base.OnViewInitialized();

            UIImageView image = this.FindName<UIImageView>("ProfileImage");
            image.Layer.BorderColor = UIColor.LightGray.CGColor;
            image.Layer.BorderWidth = 0.5f;
        }

        #endregion
    }
}