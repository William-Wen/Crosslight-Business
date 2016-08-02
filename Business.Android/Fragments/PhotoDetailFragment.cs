using System;
using Android.Runtime;
using Android.Transitions;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Android.v7;

namespace Business.Android.Fragments
{
    /// <summary>
    ///     The Activity used when viewing larger images in the ItemEditFragment, accessible
    ///     by clicking on the ImageView of the Form Builder.
    ///     The BindingProvider used for this Fragment is the ItemDetailBindingProvider,
    ///     as indicated by the ImportBindingAttribute decorator.
    ///     The RegisterNavigationAttribute is used as the navigation identifier passed in the
    ///     ItemEditorViewModel.
    /// </summary>
    [ImportBinding(typeof(ItemDetailBindingProvider))]
    [RegisterNavigation("PhotoDetail")]
    public class PhotoDetailFragment : Fragment<ItemDetailViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PhotoDetailFragment" /> class.
        /// </summary>
        public PhotoDetailFragment()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PhotoDetailFragment" /> class.
        /// </summary>
        /// <param name="javaReference">The java reference.</param>
        /// <param name="transfer">The transfer.</param>
        public PhotoDetailFragment(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the content layout identifier.
        ///     This indicates the view used, which can be found in Resource/layout folder.
        /// </summary>
        /// <value>
        ///     The content layout identifier.
        /// </value>
        protected override int ContentLayoutId
        {
            get { return Resource.Layout.view_image_activity; }
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
        }

        #endregion
    }
}