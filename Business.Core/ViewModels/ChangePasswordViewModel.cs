using System;
using Business.Models;
using Intersoft.AppFramework.Identity;
using Intersoft.Crosslight;
using Intersoft.Crosslight.ViewModels;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for Change Password screen.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.ViewModels.EditorViewModelBase{Business.Models.PasswordModel}" />
    public class ChangePasswordViewModel : EditorViewModelBase<PasswordModel>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChangePasswordViewModel" /> class.
        /// </summary>
        public ChangePasswordViewModel()
        {
            this.Title = "Change Password";
            this.Item = new PasswordModel();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the type of the form metadata associated to the editor.
        /// </summary>
        /// <value>
        ///     The type of the form metadata.
        /// </value>
        public override Type FormMetadataType
        {
            get { return typeof(ChangePasswordFormMetadata); }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Executes the save command.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        protected override async void ExecuteSave(object parameter)
        {
            this.Validate();

            if (!this.HasErrors)
            {
                IUserService userService = this.GetService<IUserService>();
                IAccountService accountService = this.GetService<IAccountService>();

                try
                {
                    this.ActivityPresenter.Show(ActivityStyle.LargeIndicator);

                    // change password
                    User user = userService.GetCurrentUser();
                    IAccount currentAccount = accountService.CreateEncryptedAccount(user.UserName, this.Item.CurrentPassword);
                    IAccount updatedAccount = accountService.CreateEncryptedAccount(user.UserName, this.Item.NewPassword);

                    await userService.ChangePasswordAsync(user.UserName, currentAccount.GetProperty(Account.PasswordHash), updatedAccount.GetProperty(Account.PasswordHash));

                    // perform re-login with the updated account
                    await accountService.SignOutAsync();
                    await accountService.SignInAsync(updatedAccount);

                    this.MessagePresenter.Show("Your password has been successfully updated.");
                    this.NavigationService.Close(new NavigationResult(NavigationResultAction.Done));
                }
                catch (Exception exception)
                {
                    this.MessagePresenter.Show(exception.GetExceptionMessage(), "Update Failed");
                }
                finally
                {
                    this.ActivityPresenter.Hide();
                }
            }
            else
                this.MessagePresenter.Show(this.ErrorMessage);
        }

        #endregion
    }
}