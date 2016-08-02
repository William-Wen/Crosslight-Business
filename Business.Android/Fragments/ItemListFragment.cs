using System;
using Android.Graphics;
using Android.Runtime;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Android;
using Intersoft.Crosslight.Android.v7;
using Intersoft.Crosslight.Android.v7.ComponentModels;
using EditAction = Intersoft.Crosslight.Android.v7.ComponentModels.EditAction;
using Android.Graphics.Drawables;
using Android.Views;

namespace Business.Android.Fragments
{
    /// <summary>
    ///     The ListFragment that displays the list of inventory items in Inventory view (page 3)
    ///     of the Business template.
    ///     This BindingProvider used for this Fragment is ItemListBindingProvider as indicated by the
    ///     ImportBindingAttribute decorator.
    /// </summary>
    [ImportBinding(typeof(ItemListBindingProvider))]
    public class ItemListFragment : RecyclerViewFragment<ItemListViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemListFragment" /> class.
        /// </summary>
        public ItemListFragment()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemListFragment" /> class.
        /// </summary>
        /// <param name="javaReference">The java reference.</param>
        /// <param name="transfer">The transfer.</param>
        public ItemListFragment(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the footer layout identifier.
        ///     This indicates the footer view used, which can be found in Resource/layout folder.
        /// </summary>
        /// <value>
        ///     The footer layout identifier.
        /// </value>
        protected override int FooterLayoutId
        {
            get { return Resource.Layout.footer_layout; }
        }

        /// <summary>
        ///     Gets the item layout identifier.
        ///     This indicates the item cell template used, which can be found in Resource/layout folder.
        /// </summary>
        /// <value>
        ///     The item layout identifier.
        /// </value>
        protected override int ItemLayoutId
        {
            get { return Resource.Layout.item_layout; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            // Defines floating action button.
            this.FloatingActionButtons.Add(new FloatingActionButton("AddItem")
            {
                Position = FloatingActionButtonPosition.BottomRight,
                IconId = Resource.Drawable.ic_add,
                Direction = FloatingActionButtonDirection.Up,
                HideOnScrollUp = true
            });

            // Defines contextual action.
            ListContextualToolbarSettings settings = this.ContextualToolbarSettings as ListContextualToolbarSettings;
            if (settings != null)
            {
                settings.Mode = ContextualMode.Default;
                settings.CheckAllEnabled = true;
                settings.MenuId = Resource.Menu.list_contextual_actions;
                settings.BarItems.Add(new BarItem("DeleteButton", CommandItemType.Trash));
                settings.CheckAllMargin = 4;
            }

            // Defines editing action from swipe gesture.
            this.EditActions.Add(new EditAction("Sold"));
            this.EditActions.Add(new EditAction("Delete", new Color(255, 0, 0)));

            // Recycler View configuration
            this.InteractionMode = ListViewInteraction.Navigation;
            this.ChoiceInputMode = ChoiceInputMode.Single;
            this.EditingOptions = EditingOptions.AllowEditing | EditingOptions.AllowMultipleSelection;

            // Image Loader settings
            this.ImageLoaderSettings.AnimateOnLoad = true;
            this.ImageLoaderSettings.CacheExpirationPolicy = CacheExpirationPolicy.AutoDetect;

            // Defines shared elements
            this.SourceSharedElementIds.Add(Resource.Id.Icon);

            this.Appearance.Padding = new Thickness(0, 8, 0, 0);
            this.Appearance.Background = new ColorDrawable(Color.WhiteSmoke);

            this.IconId = Resource.Drawable.ic_toolbar;
        }

        #endregion
    }
}