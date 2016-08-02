using System;
using System.Threading.Tasks;
using Intersoft.AppFramework.Identity;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Input;
using Intersoft.Crosslight.Services.Auth;
using Intersoft.Crosslight.ViewModels;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for the login screen.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.ViewModels.ViewModelBase" />
    public class LoginViewModel : ViewModelBase
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginViewModel" /> class.
        /// </summary>
        public LoginViewModel()
        {
            this.LoginCommand = new DelegateCommand(ExecuteLogin);
            this.SocialLoginCommand = new DelegateCommand(ExecuteSocialLogin);
        }

        #endregion

        #region Fields

        private string _password;
        private string _username;

        #endregion

        #region Properties

        public IAccountService AccountService
        {
            get { return this.GetService<IAccountService>(); }
        }

        public DelegateCommand LoginCommand { get; private set; }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    this.OnPropertyChanged("Password");
                }
            }
        }

        public DelegateCommand SocialLoginCommand { get; private set; }

        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    this.OnPropertyChanged("Username");
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Executes the login.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private async void ExecuteLogin(object parameter)
        {
            if (string.IsNullOrEmpty(this.Username) || string.IsNullOrEmpty(this.Password))
            {
                this.MessagePresenter.Show("Please enter your username and password.");
                return;
            }

            IAccount account = this.AccountService.CreateEncryptedAccount(this.Username, this.Password);

            this.ActivityPresenter.Show("Logging in...", ActivityStyle.SmallIndicatorWithText);

            try
            {
                await this.AccountService.SignInAsync(account);

                // ensure the account is authenticated and signed in
                if (this.AccountService.IsLoggedIn())
                    this.NavigateToMainViewModel();
            }
            catch (Exception authException)
            {
                this.MessagePresenter.Show(authException.GetExceptionMessage(), "Login Failed");
            }

            this.ActivityPresenter.Hide();
        }

        /// <summary>
        ///     Executes the social login.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private async void ExecuteSocialLogin(object parameter)
        {
            if (parameter == null || string.IsNullOrEmpty(parameter.ToString()))
            {
                this.MessagePresenter.Show("Social service is not specified", "Login Failed");
                return;
            }

            try
            {
                this.ActivityPresenter.Show("Logging in...", ActivityStyle.SmallIndicatorWithText);

                await this.AccountService.SignInSocialAsync(parameter.ToString(), new AuthenticateOptions(true));

                // ensure the account is authenticated and signed in
                if (this.AccountService.IsLoggedIn())
                    this.NavigateToMainViewModel();
            }
            catch (TaskCanceledException)
            {
            }
            catch (AuthenticationException ex)
            {
                if (ex.IsPermissionDenied)
                    this.MessagePresenter.Show("Please grant Facebook to access this app by changing the permission in Settings.", "Login Permission");
                else
                    this.MessagePresenter.Show(ex.GetExceptionMessage(), "Authentication Failed");
            }
            catch (Exception ex)
            {
                this.MessagePresenter.Show(ex.GetExceptionMessage(), "Login Failed");
            }
            finally
            {
                this.ActivityPresenter.Hide();
            }
        }

        /// <summary>
        ///     Called when this instance is navigated.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        public override async void Navigated(NavigatedParameter parameter)
        {
            if (parameter != null && parameter.Sender != null && parameter.Sender is NavigationViewModel)
            {
                // called from main menu, ensure logged out
                if (this.AccountService.IsLoggedIn())
                    await this.AccountService.SignOutAsync();
            }
        }

        #endregion
    }
}