using Business.ViewModels;
using Intersoft.Crosslight.iOS;

namespace Business.iOS
{
    /// <summary>
    ///     Represents ViewController for change password screen. Accessible in the Profile screen after the user
    ///     has logged in to the application.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UIFormViewController{Business.ViewModels.ChangePasswordViewModel}" />
    public class ChangePasswordViewController : UIFormViewController<ChangePasswordViewModel>
    {
    }
}