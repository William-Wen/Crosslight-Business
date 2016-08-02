using System.Linq;
using Business.DomainModels.Inventory;
using Intersoft.AppFramework;
using Intersoft.Crosslight;
using Intersoft.Crosslight.ViewModels;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel that provides a list of Categories.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.ViewModels.ListViewModelBase{Business.DomainModels.Inventory.Category}" />
    public class CategoryListViewModel : ListViewModelBase<Category>
    {
        #region Properties

        private ICategoryRepository Repository
        {
            get
            {
                if (Container.Current.CanResolve<ICategoryRepository>())
                    return Container.Current.Resolve<ICategoryRepository>();

                return new CategoryRepository(null); // for designer support
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

            this.Items = this.Repository.GetAll().OrderBy(o => o.Name).ToList();
        }

        #endregion
    }
}