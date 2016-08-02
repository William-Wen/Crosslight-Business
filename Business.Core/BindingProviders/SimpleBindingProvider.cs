using Intersoft.Crosslight;

namespace Business
{
    /// <summary>
    ///     The BindingProvider used in the Page 1 of the Business template.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.BindingProvider" />
    public class SimpleBindingProvider : BindingProvider
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimpleBindingProvider" /> class.
        /// </summary>
        public SimpleBindingProvider()
        {
            // Bind the View's Text property with ID/Outlet set to GreetingText
            // with ViewModel property GreetingText.
            this.AddBinding("GreetingLabel", BindableProperties.TextProperty, "GreetingText");
            this.AddBinding("FooterLabel", BindableProperties.TextProperty, "FooterText");
            this.AddBinding("Text1", BindableProperties.TextProperty, new BindingDescription("NewText", BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged));
            this.AddBinding("Button1", BindableProperties.CommandProperty, "ShowToastCommand");
        }

        #endregion
    }
}