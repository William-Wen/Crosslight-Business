using Business.DomainModels.Inventory;
using Intersoft.AppFramework.ViewModels;
using Intersoft.Crosslight;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for group detail screen. Applies only on Windows Store apps.
    /// </summary>
    /// <seealso
    ///     cref="Intersoft.AppFramework.ViewModels.DataGroupDetailViewModelBase{Business.DomainModels.Inventory.CategoryGroup, Business.DomainModels.Inventory.Item, BusinessApp.DomainModels.Inventory.IItemRepository}" />
    public class GroupDetailViewModel : DataGroupDetailViewModelBase<CategoryGroup, Item, IItemRepository>
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the group key.
        /// </summary>
        /// <value>
        ///     The group key.
        /// </value>
        [StateAware]
        public int GroupKey
        {
            get { return this.Group.Category.CategoryID; }
            set
            {
                ItemRepository repository = this.Repository as ItemRepository;
                if (repository != null)
                    this.Group = repository.GetCategoryGroup(value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Called when this instance is navigated.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        public override void Navigated(NavigatedParameter parameter)
        {
            base.Navigated(parameter);

            if (parameter.Data != null)
                this.Group = parameter.Data as CategoryGroup;
        }

        #endregion
    }
}