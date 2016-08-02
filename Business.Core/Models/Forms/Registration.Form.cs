using Intersoft.Crosslight.Forms;

namespace Business.Models
{
    /// <summary>
    ///     Represents a Crosslight Form Builder form metadata used in Change Password screen.
    ///     To learn more, visit http://developer.intersoftsolutions.com/display/crosslight/Building+Rich+Data+Entry+Form
    /// </summary>
    [Form(Title = "Register")]
    public class RegistrationFormMetadata
    {
        [Section(Style = SectionLayoutStyle.ImageWithFields)]
        public static GeneralSection General;

        [Section]
        public static UserDetailSection UserDetail;

        [Section(Header = "Alternate Login")]
        public static ActionSection Action;

        public class GeneralSection
        {
            [Editor(EditorType.Image)]
            [Layout(Style = LayoutStyle.DetailOnly)]
            [Image(UseCircleMask = true, Height = 80, Width = 80, Placeholder = "square_placeholder.png", AddCaption = "", EditCaption = "")]
            public static byte[] ImageData;

            [StringInput(Placeholder = "Full Name", AutoCorrection = AutoCorrectionType.No)]
            [Layout(Style = LayoutStyle.DetailOnly)]
            public static string FullName;

            [StringInput(Placeholder = "Email", InputType = InputType.EmailAddress, AutoCorrection = AutoCorrectionType.No)]
            [Layout(Style = LayoutStyle.DetailOnly)]
            public static string Email;
        }

        public class UserDetailSection
        {
            [StringInput(Placeholder = "Username", AutoCorrection = AutoCorrectionType.No)]
            [Layout(Style = LayoutStyle.DetailOnly)]
            public static string UserName;

            [Editor(EditorType.PasswordField)]
            [StringInput(Placeholder = "Password", IsSecure = true, AutoCorrection = AutoCorrectionType.No, ClearButtonVisibility = ClearButtonVisibility.WhileEditing)]
            [Layout(Style = LayoutStyle.DetailOnly)]
            public static string Password;

            [Editor(EditorType.PasswordField)]
            [StringInput(Placeholder = "Verify Password", IsSecure = true, AutoCorrection = AutoCorrectionType.No, ClearButtonVisibility = ClearButtonVisibility.WhileEditing)]
            [Layout(Style = LayoutStyle.DetailOnly)]
            [Binding(Mode = Intersoft.Crosslight.BindingMode.TwoWay, UpdateSourceTrigger = Intersoft.Crosslight.UpdateSourceTrigger.PropertyChanged)]
            public static string VerifyPassword;
        }

        public class ActionSection
        {
            [Editor(EditorType.Button)]
            [Binding(Path = "SocialLoginCommand", SourceType = BindingSourceType.ViewModel)]
            [Display(Image = "facebook_blue.png", Caption = "Login with Facebook")]
            [Button(TintColorMode = TintColorMode.TextColor, Parameter = "Facebook")]
            public static string FacebookLoginButton;
        }
    }
}

