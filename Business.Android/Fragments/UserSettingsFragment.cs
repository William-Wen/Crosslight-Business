using System;
using Android.Runtime;
using Business.ViewModels;
using Intersoft.Crosslight.Android.v7;
using Android.Views;
using Intersoft.Crosslight;

namespace Business.Android.Fragments
{
    /// <summary>
    ///     Crosslight Form Builder Fragment used in the Settings view of the Business template.
    /// </summary>
    public class UserSettingsFragment : FormFragment<UserSettingsViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserSettingsFragment" /> class.
        /// </summary>
        public UserSettingsFragment()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserSettingsFragment" /> class.
        /// </summary>
        /// <param name="javaReference">The java reference.</param>
        /// <param name="transfer">The transfer.</param>
        public UserSettingsFragment(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            this.IconId = Resource.Drawable.ic_toolbar;
            this.AddBarItem(new BarItem("SaveButton", CommandItemType.Done));
        }

        #endregion
    }
}