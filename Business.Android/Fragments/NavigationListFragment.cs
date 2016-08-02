using System;
using Android.Runtime;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Android;
using Intersoft.Crosslight.Android.v7;

namespace Business.Android.Fragments
{
    /// <summary>
    ///     The RecyclerViewFragment to be used as the Main Drawer navigation menu.
    ///     The BindingProvider used for this RecyclerViewFragment is the NavigationBindingProvider,
    ///     as indicated by the ImportBindingAttribute decorator.
    /// </summary>
    [ImportBinding(typeof(NavigationBindingProvider))]
    public class NavigationListFragment : RecyclerViewFragment<NavigationViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavigationListFragment" /> class.
        /// </summary>
        public NavigationListFragment()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavigationListFragment" /> class.
        /// </summary>
        /// <param name="javaReference">The java reference.</param>
        /// <param name="transfer">The transfer.</param>
        public NavigationListFragment(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the header layout identifier.
        ///     This indicates the header view used, which can be found in Resource/layout folder.
        /// </summary>
        /// <value>
        ///     The header layout identifier.
        /// </value>
        protected override int HeaderLayoutId
        {
            get { return Resource.Layout.drawer_header_layout; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            this.CellStyle = CellStyle.NavigationDrawer;
            this.InteractionMode = ListViewInteraction.Navigation;
            this.ShowGroupHeader = false;
        }

        #endregion
    }
}