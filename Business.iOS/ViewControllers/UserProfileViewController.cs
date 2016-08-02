using Business.ViewModels;
using Foundation;
using Intersoft.Crosslight.iOS;

namespace Business.iOS
{
    /// <summary>
    ///     Represents the ViewController for Profile screen of the Business template.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UIFormViewController{Business.ViewModels.UserProfileViewModel}" />
    [Register("UserProfileViewController")]
    public class UserProfileViewController : UIFormViewController<UserProfileViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserProfileViewController" /> class.
        /// </summary>
        public UserProfileViewController()
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

        /// <summary>
        ///     Gets the message to display when the <see cref="P:Intersoft.Crosslight.iOS.UIFormViewController`1.CurrentItem" />
        ///     property is empty.
        /// </summary>
        /// <value>
        ///     The no item message.
        /// </value>
        public override string NoItemMessage
        {
            get { return this.ViewModel.LoadUserFailedErrorMessage; }
        }

        #endregion
    }
}