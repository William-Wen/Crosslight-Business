using Intersoft.Crosslight;

namespace Business
{
    /// <summary>
    ///     The BindingProvider used in About page (Page 2) of the Business template.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.BindingProvider" />
    public class AboutBindingProvider : BindingProvider
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AboutBindingProvider" /> class.
        /// </summary>
        public AboutBindingProvider()
        {
            // Bind the View's Text property with ID/Outlet set to IntroductionLabel
            // with ViewModel property IntroductionText.
            this.AddBinding("IntroductionLabel", BindableProperties.TextProperty, "IntroductionText");
            this.AddBinding("AboutLabel", BindableProperties.TextProperty, "AboutText");
            this.AddBinding("FooterLabel", BindableProperties.TextProperty, "FooterText");
            this.AddBinding("LearnMoreButton", BindableProperties.CommandProperty, "LearnMoreCommand");
        }

        #endregion
    }
}