using Business.ViewModels;
using Intersoft.Crosslight;

namespace Business
{
    /// <summary>
    ///     The BindingProvider used in Inventory page (Page 3) of the Business template.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.BindingProvider" />
    public class ItemListBindingProvider : BindingProvider
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemListBindingProvider" /> class.
        /// </summary>
        public ItemListBindingProvider()
        {
            IApplicationContext context = this.GetService<IApplicationService>().GetContext();

            ItemBindingDescription itemBinding = new ItemBindingDescription
            {
                DisplayMemberPath = "Name", // binds Item.Name
                DetailMemberPath = "Location", // binds Item.Location
                ImageMemberPath = "ResolvedThumbnailImage", // binds Item.ResolvedThumbnailImage
                ImagePlaceholder = "item_placeholder.png" // image placeholder to use when the image is null.
            };

            itemBinding.AddBinding("TextLabel", BindableProperties.StyleAttributesProperty, new BindingDescription("Sold")
            {
                Converter = new TextLabelStyleConverter()
            });

            // Bind the View's Text property with ID/Outlet set to TableView
            // with ViewModel property Items.
            this.AddBinding("TableView", BindableProperties.ItemsSourceProperty, "Items");
            this.AddBinding("TableView", BindableProperties.ItemTemplateBindingProperty, itemBinding, true);
            this.AddBinding("TableView", BindableProperties.IsBatchUpdatingProperty, "IsBatchUpdating");
            this.AddBinding("TableView", BindableProperties.SelectedItemProperty, "SelectedItem", BindingMode.TwoWay);
            this.AddBinding("TableView", BindableProperties.SelectedItemsProperty, "SelectedItems", BindingMode.TwoWay);
            this.AddBinding("TableView", BindableProperties.IsEditingProperty, "IsEditing", BindingMode.TwoWay);
            this.AddBinding("TableView", BindableProperties.EditActionCommandProperty, "EditActionCommand", BindingMode.TwoWay);
            this.AddBinding("TableView", BindableProperties.AddItemCommandProperty, "AddCommand");
            this.AddBinding("TableView", BindableProperties.DeleteItemCommandProperty, "DeleteCommand");
            this.AddBinding("TableView", BindableProperties.RefreshCommandProperty, "RefreshCommand");

            this.AddBinding("TableView", BindableProperties.DetailNavigationTargetProperty, new NavigationTarget(typeof(ItemEditorViewModel)), true);

            this.AddBinding("AddItem", BindableProperties.CommandProperty, "AddCommand");
            this.AddBinding("AddButton", BindableProperties.CommandProperty, "AddCommand");

            this.AddBinding("DeleteButton", BindableProperties.TextProperty, "DeleteText");
            this.AddBinding("DeleteButton", BindableProperties.CommandProperty, "DeleteCommand");
            this.AddBinding("DeleteButton", BindableProperties.CommandParameterProperty, "SelectedItems");

            this.AddBinding("EditButton", BindableProperties.CommandProperty, "EditCommand");

            this.AddBinding("RefreshButton", BindableProperties.RefreshCommandProperty, "RefreshCommand");

            this.AddBinding("MarkSoldButton", BindableProperties.TextProperty, "MarkSoldText");
            this.AddBinding("MarkSoldButton", BindableProperties.CommandProperty, "MarkSoldCommand");
            this.AddBinding("MarkSoldButton", BindableProperties.CommandParameterProperty, "SelectedItems");

            this.AddBinding("FooterLabel", BindableProperties.TextProperty, "TotalItemsText");

            this.AddBinding("UpdatedLabel", BindableProperties.TextProperty, "UpdatedText");
            this.AddBinding("CountLabel", BindableProperties.TextProperty, "TotalItemsText");
            this.AddBinding("SelectionLabel", BindableProperties.TextProperty, "SelectedItemsText");
        }

        #endregion
    }
}