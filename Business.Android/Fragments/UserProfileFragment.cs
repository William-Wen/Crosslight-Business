using System;
using Android.Runtime;
using Android.Views;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Android.v7;

namespace Business.Android.Fragments
{
    /// <summary>
    ///     Crosslight Form Builder Fragment used in the Profile page of the Business template.
    /// </summary>
    public class UserProfileFragment : FormFragment<UserProfileViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserProfileFragment" /> class.
        /// </summary>
        public UserProfileFragment()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserProfileFragment" /> class.
        /// </summary>
        /// <param name="javaReference">The java reference.</param>
        /// <param name="transfer">The transfer.</param>
        public UserProfileFragment(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initialize this instance.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            this.IconId = Resource.Drawable.ic_toolbar;
            this.BarItems.Add(new BarItem("SaveButton", CommandItemType.Done));
        }

        #endregion


    }
}