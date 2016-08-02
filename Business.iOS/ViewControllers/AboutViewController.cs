using System;
using Business.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.iOS;

namespace Business.iOS
{
    /// <summary>
    ///     Represents ViewController for the About screen (page 2).
    ///     This class is decorated with the StoryboardAttribute that allows for VC initialization via universal Storyboard.
    ///     This class is decorated with the ImportBindingAttribute that indicates
    ///     the binding provider to be used with this ViewController.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UIViewController{Business.ViewModels.AboutNavigationViewModel}" />
    [Storyboard("MainStoryboard")]
    [ImportBinding(typeof(AboutBindingProvider))]
    public partial class AboutViewController : UIViewController<AboutNavigationViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AboutViewController" /> class.
        /// </summary>
        /// <param name="intPtr">The int PTR.</param>
        public AboutViewController(IntPtr intPtr)
            : base(intPtr)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether the content should be automatically resized to fit the available screen real
        ///     estate.
        /// </summary>
        /// <value>
        ///     <c>true</c> if auto fit content size; otherwise, <c>false</c>.
        /// </value>
        public override bool AutoFitContentSize
        {
            get { return true; }
        }

        #endregion
    }
}