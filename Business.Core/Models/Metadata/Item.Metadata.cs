using Business.DomainModels.Inventory;
using Intersoft.Crosslight.Data.ComponentModel;

namespace Business.Models
{
    /// <summary>
    ///     Represents a Crosslight Form Builder form validation metadata used in Add/Edit Item screen.
    ///     To learn more, visit http://developer.intersoftsolutions.com/display/crosslight/Building+Rich+Data+Entry+Form
    /// </summary>
    public class ItemMetadata
    {
        #region Properties

        [Required(ErrorMessageResourceName = "CategoryRequired")]
        public Category Category { get; set; }

        [Required(ErrorMessageResourceName = "ItemLocationRequired")]
        public string Location { get; set; }

        [Required(ErrorMessageResourceName = "ItemNameRequired")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "MinItemPrice")]
        public decimal? Price { get; set; }

        [Required(ErrorMessageResourceName = "MinItemQuantity")]
        public int? Qty { get; set; }

        #endregion
    }
}