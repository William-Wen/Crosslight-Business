using System;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.iOS;
using UIKit;

namespace Business.iOS
{
    /// <summary>
    ///     Represents ViewController for the Login screen.
    ///     This class is decorated with the StoryboardAttribute that allows for VC initialization via universal Storyboard.
    ///     This class is decorated with the ImportBindingAttribute that indicates
    ///     the binding provider to be used with this ViewController.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UIViewController{Business.ViewModels.HomeViewModel}" />
    [Storyboard("MainStoryboard")]
    [ImportBinding(typeof(LoginBindingProvider))]
    [RegisterNavigation(IsRootView = true)]
    public partial class HomeViewController : UIViewController<HomeViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HomeViewController" /> class.
        /// </summary>
        /// <param name="intPtr">The int PTR.</param>
        public HomeViewController(IntPtr intPtr)
            : base(intPtr)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether the keyboard should be automatically hidden when user tapped anywhere on the
        ///     screen.
        /// </summary>
        /// <value>
        ///     <c>true</c> if hide keyboard on tap; otherwise, <c>false</c>.
        /// </value>
        public override bool HideKeyboardOnTap
        {
            get { return true; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Called when the view is in initialization cycle.
        /// </summary>
        protected override void InitializeView()
        {
            base.InitializeView();

            this.LoginPanel.SetNeedsDisplay();
        }

        /// <summary>
        ///     Turns auto-rotation on or off.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if the <see cref="T:UIKit.UIViewController" /> should auto-rotate, <see langword="false" />
        ///     otherwise.
        /// </returns>
        public override bool ShouldAutorotate()
        {
            return false;
        }

        #endregion
    }
}