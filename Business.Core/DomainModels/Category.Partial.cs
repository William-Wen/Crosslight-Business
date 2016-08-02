using System.Runtime.Serialization;
using Intersoft.Crosslight;

namespace Business.DomainModels.Inventory
{
    /// <summary>
    ///     Class the represents inventory category.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.Data.EntityModel.EntityBase" />
    [Serializable]
    partial class Category
    {
        #region Fields

        private byte[] _largeImage;

        #endregion

        #region Properties

        [IgnoreDataMember]
        public byte[] LargeImage
        {
            get { return _largeImage; }
            set
            {
                if (_largeImage != value)
                {
                    _largeImage = value;
                    this.OnPropertyChanged("LargeImage");
                }
            }
        }

        #endregion
    }
}