using Intersoft.Crosslight;

namespace Business
{
    /// <summary>
    ///     The BindingProvider used in view larger image of the Business template, accessible
    ///     from the Item Editor view.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.BindingProvider" />
    public class ItemDetailBindingProvider : BindingProvider
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemDetailBindingProvider" /> class.
        /// </summary>
        public ItemDetailBindingProvider()
        {
            // Bind the View's Text property with ID/Outlet set to NameLabel
            // with ViewModel property Item.Name.
            this.AddBinding("NameLabel", BindableProperties.TextProperty, "Item.Name");
            this.AddBinding("ImageView", BindableProperties.ImageSourceProperty, "Item.ResolvedLargeImage");
            this.AddBinding("DescriptionLabel", BindableProperties.TextProperty, "Item.Description");
            this.AddBinding("CategoryLabel", BindableProperties.TextProperty, "Item.Category.Name");
            this.AddBinding("PurchaseDateLabel", BindableProperties.TextProperty, new BindingDescription("Item.PurchaseDate")
            {
                StringFormat = "{0:d}"
            });
            this.AddBinding("LocationLabel", BindableProperties.TextProperty, "Item.Location");
            this.AddBinding("QuantityLabel", BindableProperties.TextProperty, "Item.Quantity");
            this.AddBinding("PriceLabel", BindableProperties.TextProperty, "Item.Price");
            this.AddBinding("SerialNumberLabel", BindableProperties.TextProperty, "Item.SerialNumber");
            this.AddBinding("NotesLabel", BindableProperties.TextProperty, "Item.Notes");

            this.AddBinding("CloseButton", BindableProperties.CommandProperty, "CloseCommand");
        }

        #endregion
    }
}