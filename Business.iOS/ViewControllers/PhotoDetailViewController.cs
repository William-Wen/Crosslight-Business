using System;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.iOS;
using UIKit;

namespace Business.iOS
{
    /// <summary>
    ///     Represents the ViewController for viewing larger images, accessible from the add/edit item detail.
    ///     This class is decorated with the StoryboardAttribute that allows for VC initialization via universal Storyboard.
    ///     This class is decorated with the ImportBindingAttribute that indicates
    ///     the binding provider to be used with this ViewController.
    ///     The RegisterNavigationAttribute is used as the navigation identifier passed in the
    ///     ItemEditorViewModel.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UIViewController{Business.ViewModels.ItemDetailViewModel}" />
    [Storyboard("MainStoryboard")]
    [ImportBinding(typeof(ItemDetailBindingProvider))]
    [RegisterNavigation("PhotoDetail")]
    public partial class PhotoDetailViewController : UIViewController<ItemDetailViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PhotoDetailViewController" /> class.
        /// </summary>
        /// <param name="intPtr">The int PTR.</param>
        public PhotoDetailViewController(IntPtr intPtr)
            : base(intPtr)
        {
        }

        #endregion

        #region Fields

        private UIBarStyle _originalBarStyle;

        #endregion

        #region Methods

        /// <summary>
        ///     Called when the view will appear.
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            _originalBarStyle = this.NavigationController.NavigationBar.BarStyle;
            this.NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
        }

        /// <summary>
        ///     Called when the view is about to disappear from the screen.
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            this.NavigationController.NavigationBar.BarStyle = _originalBarStyle;
        }

        #endregion
    }
}