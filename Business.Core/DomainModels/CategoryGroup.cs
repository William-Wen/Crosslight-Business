using System.Linq;
using Intersoft.AppFramework;
using Intersoft.Crosslight;

namespace Business.DomainModels.Inventory
{
    /// <summary>
    ///     Class that represents inventory category group.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.GroupItem{Business.DomainModels.Inventory.Item}" />
    public class CategoryGroup : GroupItem<Item>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CategoryGroup" /> class.
        /// </summary>
        /// <param name="groupItem">The group item.</param>
        public CategoryGroup(IGrouping<string, Item> groupItem)
            : base(groupItem)
        {
            ICategoryRepository categoryRepository = this.CategoryRepository;
            if (categoryRepository != null)
                this.Category = categoryRepository.GetByName(groupItem.Key);
        }

        #endregion

        #region Properties

        public Category Category { get; private set; }

        private ICategoryRepository CategoryRepository
        {
            get { return Container.Current.Resolve<ICategoryRepository>(); }
        }

        #endregion
    }
}