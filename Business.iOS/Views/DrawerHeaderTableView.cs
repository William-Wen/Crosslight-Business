using System;
using Foundation;
using UIKit;

namespace Business.iOS
{
    /// <summary>
    /// 	View for the navigation drawer header.
    /// </summary>
    public partial class DrawerHeaderTableView : UIView
    {
        #region Constructors

        /// <summary>
        /// 	A constructor used when creating managed representations of unmanaged objects;  Called by the runtime.
        /// </summary>
        /// <param name="handle">Pointer (handle) to the unmanaged object.</param>
        /// <remarks>This constructor is invoked by the runtime infrastructure (<see cref="M:ObjCRuntime.GetNSObject (System.IntPtr)" />) to create a new managed representation for a pointer to an unmanaged Objective-C object.    You should not invoke this method directly, instead you should call the GetNSObject method as it will prevent two instances of a managed object to point to the same native object.</remarks>
        public DrawerHeaderTableView(IntPtr handle)
            : base(handle)
        {
        }

        #endregion

        #region Constants

        public static readonly UINib Nib = UINib.FromName("DrawerHeaderTableView", NSBundle.MainBundle);

        #endregion

        #region Methods

        /// <summary>
        /// 	Indicates an attempt to read a value of an undefined key. If not overridden, raises an NSUndefinedKeyException.
        /// </summary>
        /// <param name="key">Name of the Objective-C property.  It must be an ASCII name, start with a lowercase letter and contain no spaces.</param>
        /// <returns>NSObject.</returns>
        /// <remarks>Subclasses can override this method to return an alternate value for undefined keys. The default implementation raises an NSUndefinedKeyException.</remarks>
        public override NSObject ValueForUndefinedKey(NSString key)
        {
            return null;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// 	Creates this instance.
        /// </summary>
        /// <returns>DrawerHeaderTableView.</returns>
        public static DrawerHeaderTableView Create()
        {
            return (DrawerHeaderTableView)Nib.Instantiate(null, null)[0];
        }

        #endregion
    }
}