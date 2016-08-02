using System;
using Android.Runtime;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Android.v7;

namespace Business.Android.Fragments
{
    /// <summary>
    ///     The Fragment displayed in Page 1 of the Business template.
    ///     The BindingProvider used for this Fragment is the SimpleBindingProvider,
    ///     as indicated by the ImportBindingAttribute decorator.
    /// </summary>
    [ImportBinding(typeof(SimpleBindingProvider))]
    public class SimpleFragment : Fragment<SimpleViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimpleFragment" /> class.
        /// </summary>
        /// <param name="javaReference">The java reference.</param>
        /// <param name="transfer">The transfer.</param>
        public SimpleFragment(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimpleFragment" /> class.
        /// </summary>
        public SimpleFragment()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the content layout identifier.
        ///     This view uses the simple_fragment.axml layout found in Resources/layout folder.
        /// </summary>
        /// <value>The content layout identifier.</value>
        protected override int ContentLayoutId
        {
            get { return Resource.Layout.simple_fragment; }
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