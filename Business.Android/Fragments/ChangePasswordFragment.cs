using System;
using Android.Runtime;
using Android.Views;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Android.v7;

namespace Business.Android.Fragments
{
    /// <summary>
    ///     Crosslight Form Builder Fragment for changing password in the Profile view.
    /// </summary>
    public class ChangePasswordFragment : FormFragment<ChangePasswordViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChangePasswordFragment" /> class.
        /// </summary>
        public ChangePasswordFragment()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChangePasswordFragment" /> class.
        /// </summary>
        /// <param name="javaReference">The java reference.</param>
        /// <param name="transfer">The transfer.</param>
        public ChangePasswordFragment(IntPtr javaReference, JniHandleOwnership transfer)
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