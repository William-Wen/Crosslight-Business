using Intersoft.Crosslight.Forms;

namespace Business.Models
{
    /// <summary>
    ///     Represents a Crosslight Form Builder form metadata used in Change Password screen.
    ///     Accessible inside Profile screen. To learn more, visit http://developer.intersoftsolutions.com/display/crosslight/Building+Rich+Data+Entry+Form
    /// </summary>
    [Form(Title = "Change Password")]
    public class ChangePasswordFormMetadata
    {
        #region Fields

        [Editor(EditorType.PasswordField)]
        [StringInput(Placeholder = "Current Password", IsSecure = true, AutoCorrection = AutoCorrectionType.No, ClearButtonVisibility = ClearButtonVisibility.WhileEditing)]
        [Layout(Style = LayoutStyle.DetailOnly)]
        public static string CurrentPassword;

        [Editor(EditorType.PasswordField)]
        [StringInput(Placeholder = "New Password", IsSecure = true, AutoCorrection = AutoCorrectionType.No, ClearButtonVisibility = ClearButtonVisibility.WhileEditing)]
        [Layout(Style = LayoutStyle.DetailOnly)]
        public static string NewPassword;

        [Editor(EditorType.PasswordField)]
        [StringInput(Placeholder = "Verify New Password", IsSecure = true, AutoCorrection = AutoCorrectionType.No, ClearButtonVisibility = ClearButtonVisibility.WhileEditing)]
        [Layout(Style = LayoutStyle.DetailOnly)]
        public static string VerifyNewPassword;

        #endregion
    }
}