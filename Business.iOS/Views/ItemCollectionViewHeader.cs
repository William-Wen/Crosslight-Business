using System;
using Foundation;
using UIKit;

namespace Business.iOS
{
    /// <summary>
    /// 	Template for item collection view header.
    /// </summary>
    public partial class ItemCollectionViewHeader : UICollectionReusableView
    {
        #region Constructors

        /// <summary>
        /// 	A constructor used when creating managed representations of unmanaged objects;  Called by the runtime.
        /// </summary>
        /// <param name="handle">Pointer (handle) to the unmanaged object.</param>
        /// <remarks>This constructor is invoked by the runtime infrastructure (<see cref="M:ObjCRuntime.GetNSObject (System.IntPtr)" />) to create a new managed representation for a pointer to an unmanaged Objective-C object.    You should not invoke this method directly, instead you should call the GetNSObject method as it will prevent two instances of a managed object to point to the same native object.</remarks>
        public ItemCollectionViewHeader(IntPtr handle)
            : base(handle)
        {
        }

        #endregion

        #region Constants

        public static readonly NSString Key = new NSString("ItemCollectionViewHeader");
        public static readonly UINib Nib = UINib.FromName("ItemCollectionViewHeader", NSBundle.MainBundle);

        #endregion

        #region Static Methods

        /// <summary>
        /// 	Creates this instance.
        /// </summary>
        /// <returns>ItemCollectionViewHeader.</returns>
        public static ItemCollectionViewHeader Create()
        {
            return (ItemCollectionViewHeader)Nib.Instantiate(null, null)[0];
        }

        #endregion
    }
}