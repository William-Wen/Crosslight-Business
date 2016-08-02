using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Business.DomainModels.Inventory;
using Business.Models;
using Intersoft.AppFramework;
using Intersoft.AppFramework.Models;
using Intersoft.AppFramework.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Input;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for the Inventory item list.
    /// </summary>
    /// <seealso
    ///     cref="Intersoft.AppFramework.ViewModels.DataListViewModelBase{Business.DomainModels.Inventory.Item, Business.DomainModels.Inventory.IItemRepository}" />
    public class ItemListViewModel : DataListViewModelBase<Item, IItemRepository>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemListViewModel" /> class.
        /// </summary>
        public ItemListViewModel()
        {
            // configure data behaviors

            this.EnableRefresh = true;
            this.EnableIncrementalRefresh = true;
            this.EnableIncrementalLoading = true;
            this.EnableAsyncFilter = true;
            this.IncrementalLoadingSize = 20;
            this.ExitEditModeOnDelete = true;

            // commands
            this.NavigateGroupCommand = new DelegateCommand(ExecuteNavigateGroup);
            this.MarkSoldCommand = new DelegateCommand(ExecuteMarkSold, CanExecuteMarkSold);
            this.LoadIncrementalCommand = new DelegateCommand(ExecuteLoadIncrementalCommand, CanExecuteLoadIncrementalCommand);

            this.Title = "My Inventory";
        }

        #endregion

        #region Fields

        private ItemQueryDefinition _filterQueryDefinition;
        private DateTime? _lastUpdate;
        private ItemQueryDefinition _queryDefinition;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the filter query.
        /// </summary>
        /// <value>
        ///     The filter query.
        /// </value>
        protected override IQueryDefinition FilterQuery
        {
            get
            {
                if (_filterQueryDefinition == null)
                    _filterQueryDefinition = new ItemQueryDefinition();

                return _filterQueryDefinition;
            }
        }

        /// <summary>
        ///     Gets or sets the last update time.
        /// </summary>
        /// <value>
        ///     The last update.
        /// </value>
        public DateTime? LastUpdate
        {
            get { return _lastUpdate; }
            set
            {
                if (_lastUpdate != value)
                {
                    _lastUpdate = value;
                    OnPropertyChanged("LastUpdate");
                    OnPropertyChanged("UpdatedText");
                }
            }
        }

        public DelegateCommand LoadIncrementalCommand { get; set; }

        public DelegateCommand MarkSoldCommand { get; set; }

        public string MarkSoldText
        {
            get
            {
                if (this.SelectedItems == null || this.SelectedItems.Count() == 0)
                    return "Mark as Sold";
                return "Mark as Sold (" + this.SelectedItems.Count() + ")";
            }
        }

        public DelegateCommand NavigateGroupCommand { get; set; }

        /// <summary>
        ///     Gets the selected items text.
        /// </summary>
        /// <value>
        ///     The selected items text.
        /// </value>
        public virtual string SelectedItemsText
        {
            get
            {
                if (this.SelectedItems != null)
                {
                    if (this.SelectedItems.Count == 0)
                        return "No selected items.";
                    if (this.SelectedItems.Count == 1)
                        return "1 selected item.";
                    return this.SelectedItems.Count + " selected items.";
                }

                return "No selected items.";
            }
        }

        /// <summary>
        ///     Gets the title text.
        /// </summary>
        /// <value>
        ///     The title text.
        /// </value>
        public override string TitleText
        {
            get
            {
                if (this.Items != null)
                    return "Inventories (" + this.Items.Count() + ")";

                return "Inventories";
            }
        }

        /// <summary>
        ///     Gets the updated text.
        /// </summary>
        /// <value>
        ///     The updated text.
        /// </value>
        public virtual string UpdatedText
        {
            get
            {
                if (this.LastUpdate.HasValue)
                {
                    if (DateTime.Today == this.LastUpdate.Value.Date)
                        return "Updated " + this.LastUpdate.Value.ToString("t");
                    return "Updated " + this.LastUpdate.Value.ToString("g");
                }

                return "Never updated";
            }
        }

        /// <summary>
        ///     Gets the view query.
        /// </summary>
        /// <value>
        ///     The view query.
        /// </value>
        protected override IQueryDefinition ViewQuery
        {
            get
            {
                if (_queryDefinition == null)
                    _queryDefinition = new ItemQueryDefinition();

                return _queryDefinition;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the edit action command can be executed.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        /// <returns>
        ///   <c>true</c> if the edit action command can be executed; otherwise, <c>false</c>.
        /// </returns>
        protected override bool CanExecuteEditAction(object parameter)
        {
            return true;
        }

        /// <summary>
        ///     Determines whether this instance can execute load incremental command based on the specified paramater.
        /// </summary>
        /// <param name="paramater">The paramater.</param>
        /// <returns></returns>
        private bool CanExecuteLoadIncrementalCommand(object paramater)
        {
            return this.EnableIncrementalLoading;
        }

        /// <summary>
        ///     Determines whether this instance can execute mark sold based the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        private bool CanExecuteMarkSold(object parameter)
        {
            if (parameter is Item)
                return true;
            if (parameter is IEnumerable<Item>)
                return ((IEnumerable<Item>)parameter).Count() > 0;

            return false;
        }

        /// <summary>
        ///     Executes the add command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        protected override void ExecuteAdd(object parameter)
        {
            this.NavigationService.Navigate<ItemEditorViewModel>(new NavigationParameter
            {
                NavigationMode = NavigationMode.Modal,
                EnsureNavigationContext = true,
                ModalPresentationStyle = ModalPresentationStyle.FormSheet,
                CommandId = "Add"
            });
        }

        /// <summary>
        ///     Executes the edit command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        protected override void ExecuteEdit(object parameter)
        {
            if (this.SelectedItem != null)
                this.NavigationService.Navigate<ItemEditorViewModel>(new NavigationParameter(this.SelectedItem));
        }

        /// <summary>
        ///     Executes the edit action command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        protected override void ExecuteEditAction(object parameter)
        {
            base.ExecuteEditAction(parameter);

            EditingParameter editingParameter = parameter as EditingParameter;
            if (editingParameter != null)
            {
                if (editingParameter.CustomAction == "Sold")
                    this.ExecuteMarkSold(editingParameter.Item);
                else if (editingParameter.CustomAction == "Delete")
                    this.ExecuteDelete(editingParameter.Item);

                editingParameter.ShouldEndEditing = true;
            }
        }

        /// <summary>
        ///     Executes the load incremental command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void ExecuteLoadIncrementalCommand(object parameter)
        {
            LoadDataIncremental();
        }

        /// <summary>
        ///     Executes the mark sold command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private async void ExecuteMarkSold(object parameter)
        {
            bool result = false;

            if (parameter is IEnumerable<Item>)
            {
                IEnumerable<Item> items = parameter as IEnumerable<Item>;

                foreach (Item item in items)
                {
                    item.Sold = true;
                    item.SellDate = DateTime.Today;

                    this.OnDataChanged(item);
                    result = true;
                }
            }
            else if (parameter is Item)
            {
                var item = parameter as Item;
                item.Sold = true;
                item.SellDate = DateTime.Today;

                this.OnDataChanged(item);
                result = true;
            }

            if (result)
            {
                if (await this.SaveDataAsync(DataAction.Update))
                {
                    if (this.SelectedItems != null)
                        this.SelectedItems.Clear();

                    // automatically exit edit mode once command is successful
                    this.IsEditing = false;
                }

                if (this.SelectedItems != null)
                    this.SelectedItems.Clear();
            }
        }

        /// <summary>
        ///     Executes the navigate group action.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void ExecuteNavigateGroup(object parameter)
        {
            this.NavigationService.Navigate<GroupDetailViewModel>(new NavigationParameter(parameter));
        }

        /// <summary>
        ///     Loads the prerequisite data.
        /// </summary>
        /// <returns></returns>
        protected override async Task LoadPrerequisiteData()
        {
            ICategoryRepository categoryRepository = Container.Current.Resolve<ICategoryRepository>();
            if (categoryRepository != null)
                await categoryRepository.GetAllAsync(); // retrieves category data and stored to entity container
        }

        /// <summary>
        ///     Called when data is loaded.
        /// </summary>
        /// <param name="selectResult">The select result.</param>
        /// <param name="loadingMode">The loading mode.</param>
        protected override void OnDataLoaded(ISelectResult<Item> selectResult, DataLoadingMode loadingMode)
        {
            base.OnDataLoaded(selectResult, loadingMode);

            this.LastUpdate = DateTime.Now;
        }

        /// <summary>
        ///     Called when the selected item changed.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        protected override void OnSelectedItemChanged(Item newItem)
        {
            base.OnSelectedItemChanged(newItem);
            this.MarkSoldCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     Called when the collection of selected items changed.
        /// </summary>
        /// <param name="e">Event argument.</param>
        protected override void OnSelectedItemsCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnSelectedItemsCollectionChanged(e);

            this.OnPropertyChanged("SelectedItemsText");
            this.OnPropertyChanged("MarkSoldText");
            this.MarkSoldCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     Refreshes the group items.
        /// </summary>
        public override void RefreshGroupItems()
        {
            // Uncomment the following line to display items in plain list
            // Note: If EnableIncrementalLoading is enabled, grouping is recommended to be disabled 
            //       since it's not appropriate for user experiences

            //if (this.Items != null)
            //    this.GroupItems = this.Items.OrderBy(o => o.Category.Name).GroupBy(o => o.Category.Name).Select(o => new CategoryGroup(o)).ToList();
        }

        #endregion
    }
}