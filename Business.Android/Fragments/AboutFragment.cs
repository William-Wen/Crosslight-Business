using System;
using Android.Runtime;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Android.v7;
using Intersoft.Crosslight.Android.v7.ComponentModels;

namespace Business.Android.Fragments
{
    /// <summary>
    ///     Fragment for About view used in Business template (page 2).
    ///     The BindingProvider for this class is AboutBindingProvider as reflected
    ///     by the ImportBindingAttribute decorator.
    /// </summary>
    [ImportBinding(typeof(AboutBindingProvider))]
    public class AboutFragment : Fragment<AboutNavigationViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AboutFragment" /> class.
        /// </summary>
        public AboutFragment()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AboutFragment" /> class.
        /// </summary>
        /// <param name="javaReference">The java reference.</param>
        /// <param name="transfer">The transfer.</param>
        public AboutFragment(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the content layout identifier.
        ///     The view used for this class is the about_fragment.axml found in Resource/layout folder.
        /// </summary>
        /// <value>
        ///     The content layout identifier.
        /// </value>
        protected override int ContentLayoutId
        {
            get { return Resource.Layout.about_fragment; }
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
            this.Appearance.Padding = new Thickness(16);
        }

        #endregion
    }
}