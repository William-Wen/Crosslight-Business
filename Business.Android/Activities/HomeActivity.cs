using Android.App;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Android.v7;

namespace Business.Android.Activities
{
    /// <summary>
    ///     Login Activity.
    ///     The BindingProvider used for this Activity is the LoginBindingProvider,
    ///     as indicated by theImportBindingAttribute decorator for this class.
    ///     The RegisterNavigationAttribute is used to indicate the initial root view for the application.
    ///     If the user has logged in to the application, then MainDrawerActivity is used. See
    ///     AppService.cs for reference.
    /// </summary>
    [Activity]
    [ImportBinding(typeof(LoginBindingProvider))]
    [RegisterNavigation(IsRootView = true)]
    public class HomeActivity : AppCompatActivity<HomeViewModel>
    {
        #region Properties

        /// <summary>
        ///     Gets the content layout identifier.
        ///     The view used for this class is the home_activity.axml found in Resource/layout folder.
        /// </summary>
        /// <value>
        ///     The content layout identifier.
        /// </value>
        protected override int ContentLayoutId
        {
            get { return Resource.Layout.home_activity; }
        }

        #endregion
    }
}