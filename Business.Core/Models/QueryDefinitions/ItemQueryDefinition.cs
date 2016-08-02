using Intersoft.AppFramework;
using Intersoft.Crosslight.Data.ComponentModel;

namespace Business.Models
{
    /// <summary>
    ///     Presents item query definition used in loading a view.
    /// </summary>
    /// <seealso cref="Intersoft.AppFramework.QueryDefinitionBase" />
    public class ItemQueryDefinition : QueryDefinitionBase
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemQueryDefinition" /> class.
        /// </summary>
        public ItemQueryDefinition()
        {
            this.SortExpression = "Name";
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the query descriptor.
        /// </summary>
        /// <returns></returns>
        public override QueryDescriptor GetQueryDescriptor()
        {
            QueryDescriptor queryDescriptor = this.GetBaseQueryDescriptor();

            if (this.FilterScope == "Name")
                this.AddFilter("Name", FilterOperator.StartsWith, this.FilterQuery);
            else if (this.FilterScope == "Location")
                this.AddFilter("Location", FilterOperator.StartsWith, this.FilterQuery);

            return queryDescriptor;
        }

        #endregion
    }
}