using UIKit;

namespace Business.iOS
{
    /// <summary>
    ///     Class that represents the main entry point for iOS applications.
    /// </summary>
    public class Application
    {
        #region Methods

        private static void Main(string[] args)
        {
            // This is the main entry point of the application.
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }

        #endregion
    }
}