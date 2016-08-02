using System.Collections.Generic;
using System.Linq;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Data.EntityModel;
using Intersoft.Crosslight.RestClient;

namespace Business.DomainModels.Inventory
{
    /// <summary>
    ///     Provides methods for ItemRepository.
    /// </summary>
    /// <seealso
    ///     cref="Intersoft.AppFramework.ModelServices.IEditableEntityRepository{Business.DomainModels.Inventory.Item, System.Int32}" />
    public partial interface IItemRepository
    {
        #region Methods

        CategoryGroup GetCategoryGroup(int categoryId);
        GroupItem<Item> GetLocationGroup(string group);

        #endregion
    }

    /// <summary>
    ///     Represents repository for item.
    /// </summary>
    /// <seealso
    ///     cref="Intersoft.AppFramework.ModelServices.EditableEntityRepository{Business.DomainModels.Inventory.Item, System.Int32}" />
    /// <seealso cref="Business.DomainModels.Inventory.IItemRepository" />
    public partial class ItemRepository
    {
        #region Methods

        /// <summary>
        ///     Gets the category group.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public CategoryGroup GetCategoryGroup(int categoryId)
        {
            return this.GetAll().Where(o => o.Category.CategoryID == categoryId).GroupBy(o => o.Category.Name).Select(o => new CategoryGroup(o)).FirstOrDefault();
        }

        /// <summary>
        ///     Gets the location group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        public GroupItem<Item> GetLocationGroup(string group)
        {
            return this.GetAll().Where(o => o.Location == group).GroupBy(o => o.Location).Select(o => new GroupItem<Item>(o)).FirstOrDefault();
        }

        /// <summary>
        ///     Initializes the save request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="entities">The entities.</param>
        protected override void InitializeSaveRequest(RestRequest request, IEnumerable<IEntity> entities)
        {
            IEnumerable<Item> items = entities.OfType<Item>();

            // send the new images (both thumbnail and large) along with the save request

            foreach (Item item in items)
            {
                Item originalItem = this.GetSingle(item.ItemID);

                if (originalItem.ThumbnailImage != null)
                {
                    request.AddFile("Thumbnail", originalItem.ThumbnailImage, item.Image, "image/jpg");
                    originalItem.ThumbnailImage = null;
                }

                if (originalItem.LargeImage != null)
                {
                    request.AddFile("Large", originalItem.LargeImage, item.Image, "image/jpg");
                    originalItem.LargeImage = null;
                }
            }
        }

        #endregion
    }
}