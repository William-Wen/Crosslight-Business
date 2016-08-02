using System.Linq;

namespace Business.DomainModels.Inventory
{
    /// <summary>
    ///     Provides methods for CategoryRepository.
    /// </summary>
    /// <seealso cref="Intersoft.AppFramework.ModelServices.IEditableEntityRepository{Business.DomainModels.Inventory.Category, System.Int32}" />
    public partial interface ICategoryRepository
    {
        #region Methods

        /// <summary>
        /// Gets the Category from category name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Category GetByName(string name);

        #endregion
    }

    /// <summary>
    ///     Represents repository for item category.
    /// </summary>
    /// <seealso cref="Intersoft.AppFramework.ModelServices.EditableEntityRepository{Business.DomainModels.Inventory.Category, System.Int32}" />
    /// <seealso cref="Business.DomainModels.Inventory.ICategoryRepository" />
    public partial class CategoryRepository
    {
        #region ICategoryRepository Members

        /// <summary>
        /// Gets the category from category name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual Category GetByName(string name)
        {
            return this.GetAll().FirstOrDefault(o => o.Name == name);
        }

        #endregion
    }
}