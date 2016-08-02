using System;
using System.Threading.Tasks;
using Business.Models;
using Intersoft.AppFramework.Identity;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Input;
using Intersoft.Crosslight.ViewModels;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for the register screen.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.ViewModels.EditorViewModelBase{Intersoft.AppFramework.Identity.RegistrationData}" />
    public class RegisterViewModel : EditorViewModelBase<RegistrationData>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisterViewModel" /> class.
        /// </summary>
        public RegisterViewModel()
        {
            this.Title = "Register";
            this.Item = new RegistrationData();
            this.SocialLoginCommand = new DelegateCommand(ExecuteSocialLogin);
        }

        #endregion

        #region Properties

        public IAccountService AccountService
        {
            get { return this.GetService<IAccountService>(); }
        }

        public override Type FormMetadataType
        {
            get { return typeof(RegistrationFormMetadata); }
        }

        public DelegateCommand SocialLoginCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Executes the save command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        protected override async void ExecuteSave(object parameter)
        {
            this.Validate();

            // submit registration data to server
            if (!this.HasErrors)
            {
                IUserService service = this.GetService<IUserService>();
                IAccount account = this.AccountService.CreateEncryptedAccount(this.Item.UserName, this.Item.Password);

                string[] names = this.Item.FullName.Split(' ');
                this.Item.FirstName = names[0];
                this.Item.PasswordHash = account.GetProperty(Account.PasswordHash);

                if (names.Length > 1)
                    this.Item.LastName = names[1];
                else
                    this.Item.LastName = string.Empty;

                if (this.Item.ImageData != null)
                    this.Item.ImageUrl = Guid.NewGuid().ToString("D") + ".jpg";

                try
                {
                    this.ActivityPresenter.Show("Creating your account...", ActivityStyle.SmallIndicatorWithText);
                    await service.RegisterAsync(this.Item);
                    this.ActivityPresenter.Hide();

                    // auto login
                    this.ActivityPresenter.Show("Logging in...", ActivityStyle.SmallIndicatorWithText);

                    try
                    {
                        await this.AccountService.SignInAsync(account);

                        // ensure the account is authenticated and signed in
                        if (this.AccountService.IsLoggedIn())
                            this.NavigationService.Close(new NavigationResult(NavigationResultAction.Done));
                    }
                    catch (Exception authException)
                    {
                        this.MessagePresenter.Show(authException.GetExceptionMessage(), "Login Failed");
                    }
                    finally
                    {
                        this.ActivityPresenter.Hide();
                    }
                }
                catch (Exception exception)
                {
                    this.MessagePresenter.Show(exception.GetExceptionMessage(), "Registration Failed");
                }
                finally
                {
                    this.ActivityPresenter.Hide();
                }
            }
            else
                this.MessagePresenter.Show(this.ErrorMessage);
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
                    this.NavigationService.Close(new NavigationResult(NavigationResultAction.Done));
            }
            catch (TaskCanceledException)
            {
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

        #endregion
    }
}