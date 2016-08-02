using Business.ViewModels;
using Foundation;
using Intersoft.Crosslight;
using Intersoft.Crosslight.iOS;
using UIKit;

namespace Business.iOS
{
    /// <summary>
    ///     Represents ViewController for the navigation drawer.
    ///     The RegisterAttribute is required for Xamarin.iOS to process the ViewController normally.
    ///     The RegisterNavigationAttribute is used to indicate the initial root view for the application.
    ///     If the user has not logged in to the application, then HomeViewController is used. See
    ///     AppService.cs for reference.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UIDrawerNavigationController{Business.ViewModels.DrawerViewModel}" />
    [Register("MainDrawerViewController")]
    [RegisterNavigation(IsRootView = true)]
    public class MainDrawerViewController : UIDrawerNavigationController<DrawerViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainDrawerViewController" /> class.
        /// </summary>
        public MainDrawerViewController()
        {
            this.DrawerSettings.EnableStatusBarTransition = false;
            this.DrawerSettings.StatusBarTransitionMode = StatusBarTransitionMode.TranslucentBlur;
            this.DrawerSettings.LeftStatusBarColor = UIColor.GroupTableViewBackgroundColor;
        }

        #endregion
    }
}