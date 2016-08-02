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
    [Register("HomeViewController")]
    partial class HomeViewController
    {
        [Outlet]
        UIKit.UIButton FacebookLoginButton { get; set; }

        [Outlet]
        UIKit.UIButton LoginButton { get; set; }

        [Outlet]
        Intersoft.Crosslight.iOS.UIBlurView LoginPanel { get; set; }

        [Outlet]
        UIKit.UITextField PasswordTextField { get; set; }

        [Outlet]
        UIKit.UIButton RegisterButton { get; set; }

        [Outlet]
        UIKit.UITextField UsernameTextField { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (FacebookLoginButton != null)
            {
                FacebookLoginButton.Dispose();
                FacebookLoginButton = null;
            }

            if (LoginButton != null)
            {
                LoginButton.Dispose();
                LoginButton = null;
            }

            if (PasswordTextField != null)
            {
                PasswordTextField.Dispose();
                PasswordTextField = null;
            }

            if (RegisterButton != null)
            {
                RegisterButton.Dispose();
                RegisterButton = null;
            }

            if (UsernameTextField != null)
            {
                UsernameTextField.Dispose();
                UsernameTextField = null;
            }

            if (LoginPanel != null)
            {
                LoginPanel.Dispose();
                LoginPanel = null;
            }
        }
    }
}
