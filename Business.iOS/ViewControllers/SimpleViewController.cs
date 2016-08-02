using System;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.iOS;

namespace Business.iOS
{
    /// <summary>
    ///     Represents the ViewController for Hello World screen (page 1) of the Business template.
    ///     This class is decorated with the StoryboardAttribute that allows for VC initialization via universal Storyboard.
    ///     This class is decorated with the ImportBindingAttribute that indicates
    ///     the binding provider to be used with this ViewController.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UIViewController{Business.ViewModels.SimpleViewModel}" />
    [Storyboard("MainStoryboard")]
    [ImportBinding(typeof(SimpleBindingProvider))]
    public partial class SimpleViewController : UIViewController<SimpleViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimpleViewController" /> class.
        /// </summary>
        /// <param name="intPtr">The int PTR.</param>
        public SimpleViewController(IntPtr intPtr)
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
    }
}