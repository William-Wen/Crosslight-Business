using Intersoft.Crosslight;

namespace Business
{
    /// <summary>
    ///     The BindingProvider used in the drawer's navigation menu.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.BindingProvider" />
    public class NavigationBindingProvider : BindingProvider
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavigationBindingProvider" /> class.
        /// </summary>
        public NavigationBindingProvider()
        {
            ItemBindingDescription itemBinding = new ItemBindingDescription
            {
                DisplayMemberPath = "Title",
                NavigateMemberPath = "Target"
            };

            // Bind the View's ItemsSource property with ID/Outlet set to TableView
            // with ViewModel property Items.
            this.AddBinding("TableView", BindableProperties.ItemsSourceProperty, "Items");
            this.AddBinding("TableView", BindableProperties.ItemTemplateBindingProperty, itemBinding, true);
            this.AddBinding("TableView", BindableProperties.SelectedItemProperty, "SelectedItem", BindingMode.TwoWay);

            this.AddBinding("EmailDisplayLabel", BindableProperties.TextProperty, "EmailDisplay");
            this.AddBinding("LoginDisplayLabel", BindableProperties.TextProperty, "LoginDisplay");
            this.AddBinding("ProfileImage", BindableProperties.ImageSourceProperty, "ProfilePhotoUrl");
        }

        #endregion
    }
}