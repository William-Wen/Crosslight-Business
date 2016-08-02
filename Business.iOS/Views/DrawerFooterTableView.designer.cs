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
    [Register("DrawerFooterTableView")]
    partial class DrawerFooterTableView
    {
        [Outlet]
        UIKit.UILabel FooterLabel { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (FooterLabel != null)
            {
                FooterLabel.Dispose();
                FooterLabel = null;
            }
        }
    }
}
