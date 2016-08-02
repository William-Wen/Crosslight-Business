using System;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Forms;
using Business.ViewModels;

namespace Business.Models
{
    /// <summary>
    ///     Represents a Crosslight Form Builder form metadata used in Profile screen.
    ///     To learn more, visit http://developer.intersoftsolutions.com/display/crosslight/Building+Rich+Data+Entry+Form
    /// </summary>
    [Form(Title = "Edit Profile")]
    public class UserProfileFormMetadata
    {
        [Section(Style = SectionLayoutStyle.ImageWithFields)]
        public static GeneralSection General;

        [Section]
        public static UserDetailSection UserDetail;

        [Section]
        public static ChangePasswordSection ChangePassword;

        public class GeneralSection
        {
            [Editor(EditorType.Image)]
            [Image(UseCircleMask = true, Height = 80, Width = 80, FramePadding = 6, Placeholder = "square_placeholder.png")]
            [ImagePicker(ImageResultMode = ImageResultMode.Thumbnail, PickerResultCommand = "FinishImagePickerCommand")]
            [Binding(Path = "UserDetail.FormImageUrl")]
            [Layout(Style = LayoutStyle.DetailOnly)]
            public static string ImageUrl;

            [StringInput(Placeholder = "First Name", AutoCorrection = AutoCorrectionType.No)]
            [Layout(Style = LayoutStyle.DetailOnly)]
            [Binding(Path = "UserDetail.FirstName")]
            public static string FirstName;

            [StringInput(Placeholder = "Last Name", AutoCorrection = AutoCorrectionType.No)]
            [Layout(Style = LayoutStyle.DetailOnly)]
            [Binding(Path = "UserDetail.LastName")]
            public static string LastName;
        }

        public class UserDetailSection
        {
            [Editor(EditorType.TextField, IsEnabled = false)]
            [Display(Caption = "Username")]
            public static string UserName;

            [StringInput(Placeholder = "Email", InputType = InputType.EmailAddress, AutoCorrection = AutoCorrectionType.No)]
            public static string Email;

            [Display(Caption = "Birth Date")]
            [Editor(EditorType.Date)]
            [Binding(Path = "UserDetail.BirthDate", StringFormat = "{0:d}")]
            public static DateTime? BirthDate;
        }

        public class ChangePasswordSection
        {
            [Display(Caption = "Change Password")]
            [Editor(EditorType.Navigation)]
            [NavigateAction(typeof(ChangePasswordViewModel))]
            [EnabledBinding(Path = "IsWebApiAccount", SourceType = BindingSourceType.ViewModel)]
            public static string ChangePassword;
        }
    }
}
