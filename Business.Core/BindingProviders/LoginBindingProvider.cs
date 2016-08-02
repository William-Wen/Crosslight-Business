using Intersoft.Crosslight;

namespace Business
{
    /// <summary>
    ///     The BindingProvider used in Login page of the Business template.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.BindingProvider" />
    public class LoginBindingProvider : BindingProvider
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginBindingProvider" /> class.
        /// </summary>
        public LoginBindingProvider()
        {
            // Bind the View's Text property with ID/Outlet set to UsernameTextField
            // with ViewModel property Username with BindingMode set as two-way binding.
            this.AddBinding("UsernameTextField", BindableProperties.TextProperty, "Username", BindingMode.TwoWay);
            this.AddBinding("UsernameTextField", BindableProperties.HideKeyboardOnReturnProperty, true, true);
            this.AddBinding("UsernameTextField", BindableProperties.NextInputFocusProperty, "PasswordTextField", true);

            this.AddBinding("PasswordTextField", BindableProperties.TextProperty, "Password", BindingMode.TwoWay);
            this.AddBinding("PasswordTextField", BindableProperties.HideKeyboardOnReturnProperty, true, true);
            this.AddBinding("PasswordTextField", BindableProperties.CommandProperty, "LoginCommand");

            this.AddBinding("LoginButton", BindableProperties.CommandProperty, "LoginCommand");
            this.AddBinding("LoginButton", BindableProperties.HideKeyboardOnReturnProperty, true, true);

            this.AddBinding("FacebookLoginButton", BindableProperties.CommandProperty, "SocialLoginCommand");
            this.AddBinding("FacebookLoginButton", BindableProperties.CommandParameterProperty, "Facebook", true);
            this.AddBinding("FacebookLoginButton", BindableProperties.HideKeyboardOnReturnProperty, true, true);

            this.AddBinding("RegisterButton", BindableProperties.CommandProperty, "RegisterCommand");
            this.AddBinding("RegisterButton", BindableProperties.HideKeyboardOnReturnProperty, true, true);
        }

        #endregion
    }
}