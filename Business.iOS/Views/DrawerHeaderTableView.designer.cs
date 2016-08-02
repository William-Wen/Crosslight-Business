// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Business.iOS
{
    [Register("DrawerHeaderTableView")]
    partial class DrawerHeaderTableView
    {
        [Outlet]
        UIKit.UILabel LoginDisplayLabel { get; set; }

        [Outlet]
        UIKit.UIImageView ProfileImage { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (LoginDisplayLabel != null)
            {
                LoginDisplayLabel.Dispose();
                LoginDisplayLabel = null;
            }

            if (ProfileImage != null)
            {
                ProfileImage.Dispose();
                ProfileImage = null;
            }
        }
    }
}
