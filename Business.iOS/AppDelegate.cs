using Foundation;
using Intersoft.Crosslight.iOS;
using UIKit;

namespace Business.iOS
{
    /// <summary>
    ///     The main delegate for the application. This class is responsible for launching the
    ///     User Interface of the application, as well as listening (and optionally responding) to
    ///     application events from iOS.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UIPushApplicationDelegate" />
    [Register("AppDelegate")]
    public partial class AppDelegate : UIPushApplicationDelegate
    {
        #region Methods

        /// <summary>
        ///     Wraps the content view controller into the root view controller.
        /// </summary>
        /// <param name="contentViewController">Content view controller.</param>
        /// <returns>
        ///     The root view controller.
        /// </returns>
        protected override UIViewController WrapRootViewController(UIViewController contentViewController)
        {
            // This template doesn't use navigation controller, both login and main UI are root views
            // Return the content view directly
            return contentViewController;
        }

        #endregion
    }
}