using System;
using System.Runtime.Serialization;
using Business.Models;
using Intersoft.AppFramework;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Data;

namespace Business.DomainModels.Inventory
{
    /// <summary>
    ///     Class that represents inventory item.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.Data.EntityModel.EntityBase" />
    [EntityMetadata(typeof(ItemMetadata))]
    [Serializable]
    partial class Item
    {
        #region Fields

        private byte[] _largeImage;
        private AppSettings _settings;
        private byte[] _thumbnailImage;

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

        [IgnoreDataMember]
        public string ResolvedLargeImage
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Image))
                    return this.Settings.BaseImageUrl + "large/" + this.Image;

                return string.Empty;
            }
        }

        [IgnoreDataMember]
        public string ResolvedThumbnailImage
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Image))
                    return this.Settings.BaseImageUrl + "thumbs/" + this.Image;

                return string.Empty;
            }
        }

        private AppSettings Settings
        {
            get
            {
                if (_settings == null)
                    _settings = Container.Current.Resolve<AppSettings>();

                return _settings;
            }
        }

        [IgnoreDataMember]
        public byte[] ThumbnailImage
        {
            get { return _thumbnailImage; }
            set
            {
                if (_thumbnailImage != value)
                {
                    _thumbnailImage = value;
                    this.OnPropertyChanged("ThumbnailImage");
                }
            }
        }

        #endregion

        #region Methods

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "Sold")
            {
                if (this.Sold)
                {
                    if (this.SellDate == null)
                        this.SellDate = DateTime.Today;
                }
            }
        }

        #endregion
    }
}