using System;
using Android.App;
using Android.Runtime;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Android.v7;

namespace Business.Android.Activities
{
    /// <summary>
    ///     Main Drawer Activity.
    ///     The RegisterNavigationAttribute is used to indicate the initial root view for the application.
    ///     If the user has not logged in to the application, then HomeActivity is used. See
    ///     AppService.cs for reference.
    /// </summary>
    [Activity]
    [RegisterNavigation(IsRootView = true)]
    public class MainDrawerActivity : DrawerActivity<DrawerViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainDrawerActivity" /> class.
        /// </summary>
        public MainDrawerActivity()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainDrawerActivity" /> class.
        /// </summary>
        /// <param name="javaReference">The java reference.</param>
        /// <param name="transfer">The transfer.</param>
        public MainDrawerActivity(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        #endregion
    }
}