using Business.ViewModels;
using Foundation;
using Intersoft.Crosslight.iOS;

namespace Business.iOS
{
    /// <summary>
    ///     Represents a ViewController for the Settings screen.
    ///     The RegsiterAttribute is required for Xamarin.iOS to process the ViewController normally.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UIFormViewController{Business.ViewModels.UserSettingsViewModel}" />
    [Register("UserSettingsViewController")]
    public class UserSettingsViewController : UIFormViewController<UserSettingsViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserSettingsViewController" /> class.
        /// </summary>
        public UserSettingsViewController()
        {
            this.CancelButtonVisibility = CancelButtonVisibility.Always;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether the keyboard should be automatically hidden on scroll.
        /// </summary>
        /// <value>
        ///     true
        /// </value>
        /// <c>false</c>
        public override bool HideKeyboardOnScroll
        {
            get { return true; }
        }

        #endregion
    }
}