using System;
using Android.Runtime;
using Android.Views;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Android.v7;

namespace Business.Android.Fragments
{
    /// <summary>
    ///     Crosslight Form Builder Fragment to edit item in Inventory view (page 3) of the Business template.
    /// </summary>
    public class ItemEditFragment : FormFragment<ItemEditorViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemEditFragment" /> class.
        /// </summary>
        public ItemEditFragment()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemEditFragment" /> class.
        /// </summary>
        /// <param name="javaReference">The java reference.</param>
        /// <param name="transfer">The transfer.</param>
        public ItemEditFragment(IntPtr javaReference, JniHandleOwnership transfer)
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

            this.SharedImageIndex = 0;

            this.IconId = Resource.Drawable.ic_toolbar;

            this.AddBarItem(new BarItem("SaveButton", CommandItemType.Done));
        }

        #endregion
    }
}