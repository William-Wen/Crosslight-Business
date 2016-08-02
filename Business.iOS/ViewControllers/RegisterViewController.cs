using Business.ViewModels;
using Foundation;
using Intersoft.Crosslight.iOS;

namespace Business.iOS
{
    /// <summary>
    ///     Represents ViewController for the user registration screen.
    ///     The RegisterAttribute is required for Xamarin.iOS to process the ViewController normally.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UIFormViewController{Business.ViewModels.RegisterViewModel}" />
    [Register("RegisterViewController")]
    public class RegisterViewController : UIFormViewController<RegisterViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisterViewController" /> class.
        /// </summary>
        public RegisterViewController()
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