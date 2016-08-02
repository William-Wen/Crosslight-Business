using System.Linq;
using Business.DomainModels.Inventory;
using Intersoft.AppFramework.ViewModels;
using Intersoft.Crosslight;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for the Inventory item detail.
    /// </summary>
    /// <seealso
    ///     cref="Intersoft.AppFramework.ViewModels.DataDetailViewModelBase{Business.DomainModels.Inventory.Item, Business.DomainModels.Inventory.IItemRepository}" />
    public class ItemDetailViewModel : DataDetailViewModelBase<Item, IItemRepository>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Business.ViewModels.ItemDetailViewModel"/> class.
        /// </summary>
        public ItemDetailViewModel()
        {
            this.Title = "View Image";
        }

        #endregion

        #region Fields

        private GroupItem<Item> _group;

        #endregion

        #region Properties

        public GroupItem<Item> Group
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;
                    this.OnPropertyChanged("Group");
                }
            }
        }

        [StateAware]
        public int ItemKey
        {
            get { return this.Item.ItemID; }
            set
            {
                if (value == 0)
                    this.Item = this.Repository.GetAll().First();
                else
                    this.Item = this.Repository.GetSingle(this.Item.ItemID);

                if (this.Item != null)
                {
                    ItemRepository repository = this.Repository as ItemRepository;
                    if (repository != null)
                        this.Group = repository.GetCategoryGroup(this.Item.CategoryID);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Called when the add command is executed.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        protected override void ExecuteAdd(object parameter)
        {
            this.NavigationService.Navigate<ItemEditorViewModel>(new NavigationParameter
            {
                NavigationMode = NavigationMode.Modal
            });
        }

        /// <summary>
        ///     Called when the edit command is executed.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        protected override void ExecuteEdit(object parameter)
        {
            this.NavigationService.Navigate<ItemEditorViewModel>(new NavigationParameter(this.Item)
            {
                NavigationMode = NavigationMode.Modal
            });
        }

        /// <summary>
        ///     Called when this instance is navigated.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        public override void Navigated(NavigatedParameter parameter)
        {
            base.Navigated(parameter);

            if (parameter.Data != null)
            {
                ItemRepository repository = this.Repository as ItemRepository;
                if (repository != null)
                    this.Group = repository.GetCategoryGroup(this.Item.CategoryID);
            }
        }

        #endregion
    }
}