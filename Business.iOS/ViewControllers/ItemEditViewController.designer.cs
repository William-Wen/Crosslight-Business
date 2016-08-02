// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;

namespace Business.iOS
{
    [Register("ItemEditViewController")]
    partial class ItemEditViewController
    {
        [Outlet]
        UIKit.UITableViewCell NameCell { get; set; }

        [Outlet]
        UIKit.UITableViewCell LocationCell { get; set; }

        [Outlet]
        UIKit.UITableViewCell NameEditCell { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (NameCell != null)
            {
                NameCell.Dispose();
                NameCell = null;
            }

            if (LocationCell != null)
            {
                LocationCell.Dispose();
                LocationCell = null;
            }

            if (NameEditCell != null)
            {
                NameEditCell.Dispose();
                NameEditCell = null;
            }
        }
    }
}
