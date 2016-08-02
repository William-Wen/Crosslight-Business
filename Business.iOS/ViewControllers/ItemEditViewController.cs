using Business.ViewModels;
using CoreGraphics;
using Intersoft.Crosslight;
using Intersoft.Crosslight.iOS;

namespace Business.iOS
{
    /// <summary>
    ///     Represents ViewController for the Add/Edit item screen.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.iOS.UIFormViewController{Business.ViewModels.ItemEditorViewModel}" />
    public partial class ItemEditViewController : UIFormViewController<ItemEditorViewModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemEditViewController" /> class.
        /// </summary>
        public ItemEditViewController()
        {
            // Uncomment for iOS6
            // this.ContentSizeForViewInPopover = new SizeF(350f, 550f);

            this.PreferredContentSize = new CGSize(350f, 550f);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Determines the navigation mode based on the given parameter.
        /// </summary>
        /// <param name="parameter">Navigation parameter.</param>
        public override void DetermineNavigationMode(NavigationParameter parameter)
        {
            if (this.GetService<IApplicationService>().GetContext().Device.Kind == DeviceKind.Tablet)
            {
                // Only customize the navigation mode for editing (default navigation command)
                if (parameter.CommandId == null)
                {
                    parameter.PreferPopover = true;
                    parameter.EnsureNavigationContext = true;
                }
            }
        }

        #endregion
    }
}