using System;
using System.Threading.Tasks;
using Business.Models;
using Intersoft.AppFramework;
using Intersoft.AppFramework.Identity;
using Intersoft.AppFramework.Models;
using Intersoft.AppFramework.Services;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Data.ComponentModel;
using Intersoft.Crosslight.Services.Auth;
using Intersoft.Crosslight.Services.Social;
using Intersoft.Crosslight.ViewModels;

namespace Business.ViewModels
{
    /// <summary>
    ///     ViewModel for the navigation drawer.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.ViewModels.DrawerViewModelBase" />
    public class DrawerViewModel : DrawerViewModelBase
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DrawerViewModel" /> class.
        /// </summary>
        public DrawerViewModel()
        {
            EventAggregator.Default.Subscribe<UserChangedEvent, User>(OnUserChanged);

            this.LeftViewModel = new NavigationViewModel();
            this.CenterViewModel = new SimpleViewModel();

            // Uncomment to show the drawer on first load
            // this.Open(DrawerSide.Left);
        }

        #endregion

        #region Properties

        public bool IsDataSynchronizationEnabled
        {
            get { return Container.Current.Resolve<AppSettings>().EnableDataSynchronization; }
        }

        private ISynchronizationService SynchronizationService
        {
            get { return this.GetService<ISynchronizationService>(); }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Called when this instance is disposing.
        /// </summary>
        /// <param name="isDisposing">If set to <c>true</c> is disposing.</param>
        protected override void Dispose(bool isDisposing)
        {
            EventAggregator.Default.Unsubscribe<UserChangedEvent, User>(OnUserChanged);

            base.Dispose(isDisposing);
        }

        /// <summary>
        ///     Forces the logout.
        /// </summary>
        private async void ForceLogout()
        {
            var accountService = this.GetService<IAccountService>();

            // user has revoked the account, logout immediately!
            await this.MessagePresenter.ShowAsync("Your account permission for this app has been revoked. You will be required to login again.", "Account Verification", new[] { "OK" });

            await accountService.SignOutAsync();

            // redirect back to the home view
            this.NavigationService.Navigate<HomeViewModel>();
        }

        /// <summary>
        ///     Initializes the data synchronization.
        /// </summary>
        private void InitializeDataSynchronization()
        {
            if (!this.IsDataSynchronizationEnabled)
                return;

            // perform data sync only when user has logged-in to the app
            var user = this.GetService<IUserService>().GetCurrentUser();
            if (user == null)
                return;

            // configure query definition for local data retrieval
            // load only current user data for best performance and scalability

            LocalTypeQueryDefinition queryDefinition = new LocalTypeQueryDefinition();
            QueryDescriptor queryDescriptor = new QueryDescriptor();

            queryDescriptor.FilterDescriptors.Add(new FilterDescriptor("CreatedBy", FilterOperator.IsEqualTo, user.Id));

            // TODO: Add the entity types associated to the query descriptor
            // queryDefinition.AddQuery(typeof(EntityType), queryDescriptor);

            // set as the default query for synchronization service
            this.SynchronizationService.DefaultQueryDefinition = queryDefinition;

            Task task = TaskEx.Run(async () => { await this.SynchronizationService.SynchronizeDataAsync(SynchronizeAction.LoadLocalData); });

            // load local data synchronously to ensure the view is displayed with items immediately
            task.Wait();

            // perform two-way synchronization asynchronously,
            // user doesn't need to aware of this process
            this.SynchronizationService.SynchronizeDataAsync(SynchronizeAction.SyncData);
        }

        /// <summary>
        ///     Initializes the settings.
        /// </summary>
        private async void InitializeSettings()
        {
            var userSettings = await this.GetService<IUserSettingsService>().Load<UserSettings>();
            Container.Current.RegisterInstance(userSettings);
        }

        /// <summary>
        ///     Initializes the user.
        /// </summary>
        private void InitializeUser()
        {
            var service = this.GetService<IUserService>();
            var user = service.GetCurrentUser();

            if (user == null)
            {
                var task = TaskEx.Run(async () => await service.GetCachedUserAsync());
                user = task.WaitForResult();
            }
            else
            {
                // user has been initialized earlier, i.e., due to login or new registration
                // therefore, ensure user-related processes are executed once at startup.
                this.OnUserChanged(user);
            }

            if (user != null)
            {
                service.SetCurrentUser(user);

                // always ensure user record is latest
                if (!user.UserDetail.IsVerified)
                    this.RefreshUser();
            }
            else
            {
                // re-fetch user
                this.RefreshUser();
            }
        }

        /// <summary>
        ///     Called when this ViewModel is being navigated.
        /// </summary>
        /// <param name="parameter">The navigation parameter.</param>
        public override void Navigated(NavigatedParameter parameter)
        {
            // user should already be authenticated at this point,
            // verify if the user's access token is still valid
            this.VerifyAccount();

            // initialize settings
            this.InitializeSettings();

            // set initial user
            this.InitializeUser();

            // since important initialization should be executed first, 
            // call base in the last order.
            base.Navigated(parameter);
        }

        /// <summary>
        ///     Called when <see cref="UserChangedEvent" /> is triggered.
        /// </summary>
        /// <param name="user">The new user instance.</param>
        protected virtual void OnUserChanged(User user)
        {
            if (user != null)
            {
                var appSettings = Container.Current.Resolve<AppSettings>();

                // Push notification registration
                if (appSettings.EnablePushNotification)
                {
                    if (appSettings.PushRegistrationMode == PushRegistrationMode.OnUserLogin)
                    {
                        var pushRegistrationService = this.GetService<IPushRegistrationService>();
                        pushRegistrationService.RegisterPushNotificationAsync();
                    }
                }

                // perform data sync
                this.InitializeDataSynchronization();

                // refresh user info in the drawer UI
                ((NavigationViewModel)this.LeftViewModel).InitializeUser();
            }
        }

        /// <summary>
        ///     Refreshes the user.
        /// </summary>
        private async void RefreshUser()
        {
            var service = this.GetService<IUserService>();
            var account = this.GetService<IAccountService>().GetAccount();

            try
            {
                User user = await this.GetService<IUserService>().GetUserAsync(account);

                service.SetCurrentUser(user);
            }
            catch (Exception ex)
            {
                if (!this.IsDataSynchronizationEnabled)
                    this.MessagePresenter.Show(ex.GetExceptionMessage(), "Load user failed.");

                // user is required since most data relies on user's id
                if (service.GetCurrentUser() == null)
                    this.MessagePresenter.Show("Internet connection is required to use this app for the first time.");
            }
        }

        /// <summary>
        ///     Verifies the account.
        /// </summary>
        private async void VerifyAccount()
        {
            var accountService = this.GetService<IAccountService>();

            try
            {
                await accountService.VerifyAsync();
            }
            catch (Exception ex)
            {
                bool accessRevoked = false;
                if (ex is SocialException)
                {
                    SocialException socialException = ex as SocialException;
                    if (socialException.Kind == SocialExceptionKind.Unauthorized || socialException.Kind == SocialExceptionKind.Forbidden)
                        accessRevoked = true;
                }
                else if (ex is AuthenticationException)
                    accessRevoked = true;

                if (accessRevoked)
                    this.ForceLogout();
            }
        }

        #endregion
    }
}