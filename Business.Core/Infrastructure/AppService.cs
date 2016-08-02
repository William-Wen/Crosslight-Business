using System;
using System.Reflection;
using Business.DomainModels.Inventory;
using Business.ViewModels;
using Intersoft.AppFramework;
using Intersoft.AppFramework.Identity;
using Intersoft.AppFramework.PushNotification;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Containers;
using Intersoft.Crosslight.Data;
using Intersoft.Crosslight.Data.EntityModel;
using Intersoft.Crosslight.RestClient;
using Intersoft.Crosslight.Services;
using Intersoft.Crosslight.Services.Auth;
using Intersoft.Crosslight.Services.PushNotification;
using Intersoft.Crosslight.Services.Social;
using Intersoft.Crosslight.Services.Social.Networks;

namespace Business.Infrastructure
{
    /// <summary>
    ///     Crosslight's shared application initializer.
    ///     This is the perfect place to register repositories, custom services, and other dependencies via IoC.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.ApplicationServiceBase" />
    public sealed class CrosslightAppAppService : ApplicationServiceBase
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CrosslightAppAppService" /> class.
        /// </summary>
        /// <param name="context">
        ///     The application context that implements <see cref="T:Intersoft.Crosslight.IApplicationContext" />
        /// </param>
        public CrosslightAppAppService(IApplicationContext context)
            : base(context)
        {
            // To learn more about social network integrations,
            // please refer to http://developer.intersoftsolutions.com/display/crosslight/Integration+with+Social+Networks

            const string facebookAppId = "337046209753532";
            const string googleProjectId = "<your googleProjectId>";
            const string twitterConsumerKey = "<your twitterConsumerKey>";
            const string twitterConsumerSecret = "<your twitterConsumerSecret>";

            // configure app settings
            AppSettings appSettings = new AppSettings();
            appSettings.SingleSignOnAppId = "Business";
            appSettings.WebServerUrl = "http://192.168.0.16:63996";
            appSettings.BaseAppUrl = appSettings.WebServerUrl;
            appSettings.BaseImageUrl = appSettings.BaseAppUrl + "/images/";
            appSettings.RestServiceUrl = appSettings.BaseAppUrl + "/data/Inventory";
            appSettings.IdentityServiceUrl = appSettings.BaseAppUrl + "/data/Identity";
            appSettings.PushNotificationServiceUrl = appSettings.BaseAppUrl + "/data/PushNotification";
            appSettings.RequiresInternetConnection = true;

            // To learn more how to configure push notification and the server-side requirements,
            // please refer to http://developer.intersoftsolutions.com/display/crosslight/Handling+Push+Notifications

            // Uncomment the following code to enable push notification
            // appSettings.EnablePushNotification = true;
            // appSettings.PushRegistrationMode = PushRegistrationMode.OnUserLogin;

            // shared services registration
            this.GetService<ITypeResolverService>().Register(typeof(CrosslightAppAppService).GetTypeInfo().Assembly);

            // components specific registration
            this.GetService<IActivatorService>().Register<IRestClient>(c =>
            {
                RestClient restClient = new RestClient(appSettings.RestServiceUrl);
                restClient.TypeResolver = new EntityTypeResolver();
                restClient.AuthenticationServiceId = this.AccountService.ServiceId;
                restClient.AuthenticationUrl = appSettings.AuthenticationUrl;
                restClient.Account = this.AccountService.GetAccount();
                return restClient;
            });

            // application-specific containers registration
            // such as data repositories and account services
            Container.Current.RegisterInstance(appSettings);
            Container.Current.Register<IEntityContainer>("Default", c => new EntityContainer()).WithLifetimeManager(new ContainerLifetime());
            Container.Current.Register<IUserRepository, UserRepository>().WithLifetimeManager(new ContainerLifetime());
            Container.Current.Register<IPushNotificationRepository, PushNotificationRepository>().WithLifetimeManager(new ContainerLifetime());

            // for best practices, data repositories shouldn't use life-time container
            Container.Current.Register<IItemRepository>(c => new ItemRepository(c.Resolve<IEntityContainer>("Default")));
            Container.Current.Register<ICategoryRepository>(c => new CategoryRepository(c.Resolve<IEntityContainer>("Default")));

            // add new services (extensions)
            ServiceProvider.AddService<IUserService, UserService>();
            ServiceProvider.AddService<IUserSettingsService, UserSettingsService>();
            ServiceProvider.AddService<IValidationService, ValidationService>();
            ServiceProvider.AddService<IResourceLoaderService, ResourceLoaderService>();
            ServiceProvider.AddService<IResourceCacheService, ResourceCacheService>();
            ServiceProvider.AddService<IImageLoaderService, ImageLoaderService>();
            ServiceProvider.AddService<IAccountService, WebApiAccountService>();
            ServiceProvider.AddService<IAuthenticationService, AuthenticationService>();
            ServiceProvider.AddService<ISocialNetworkService, SocialNetworkService>();
            ServiceProvider.AddService<IPushNotificationService, PushNotificationService>();
            ServiceProvider.AddService<IPushRegistrationService, PushRegistrationService>();
            ServiceProvider.AddService<ILocalizationService, LocalizationService>();

            // perform additional initialization for the new services such as social and push services
            ISocialNetworkService socialService = ServiceProvider.GetService<ISocialNetworkService>();
            socialService.Register(() => new FacebookSocialNetwork(facebookAppId, "email, publish_actions"));
            socialService.Register(() => new TwitterSocialNetwork(twitterConsumerKey, twitterConsumerSecret, new Uri("myapp://connect")));

            IPushNotificationService pushNotificationService = ServiceProvider.GetService<IPushNotificationService>();
            pushNotificationService.Initialize(new PushNotificationSettings
            {
                GoogleProjectId = googleProjectId,
                WindowsPhoneChannelName = "CrosslightBusinessAppChannel"
            });
        }

        #endregion

        #region Properties

        private IAccountService AccountService
        {
            get { return ServiceProvider.GetService<IAccountService>(); }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Called when the app receives a <see cref="T:Intersoft.Crosslight.DeviceToken" /> as the result of successful push
        ///     service registration.
        /// </summary>
        /// <param name="deviceToken">Device token.</param>
        protected override async void OnDeviceTokenReceived(DeviceToken deviceToken)
        {
            base.OnDeviceTokenReceived(deviceToken);

            var pushRegistrationService = this.GetService<IPushRegistrationService>();
            var userService = this.GetService<IUserService>();
            var user = userService.GetCurrentUser();

            // device token is received from Platform Store Service
            // now check if the token and user has been registered in our app
            if (await pushRegistrationService.ShouldRegisterDeviceTokenAsync(deviceToken))
            {
                await pushRegistrationService.SaveDeviceTokenAsync(deviceToken, user, false);
                await pushRegistrationService.RegisterDeviceTokenAsync(deviceToken, user);
            }
        }

        /// <summary>
        ///     Called when a <see cref="T:Intersoft.Crosslight.Notification" /> is received.
        /// </summary>
        /// <param name="notification">The received notification.</param>
        protected override void OnNotificationReceived(Notification notification)
        {
            base.OnNotificationReceived(notification);

            // Write your code to handle push notification

            // The following sample code simply presents the received notification to the UI
            var presenterService = this.GetService<IPresenterService>();
            if (presenterService != null)
                presenterService.GetPresenter<IToastPresenter>().Show(notification.Message, notification.Title);
        }

        /// <summary>
        ///     Called when the application is starting.
        /// </summary>
        /// <param name="parameter">The startup parameters.</param>
        protected override void OnStart(StartParameter parameter)
        {
            base.OnStart(parameter);

            var appSettings = Container.Current.Resolve<AppSettings>();

            // Initialize the account service
            this.AccountService.Initialize(typeof(LoginViewModel));

            // Set the root ViewModel to be displayed at startup
            if (!this.AccountService.IsLoggedIn())
            {
                // Not logged-in, redirect user to the home page
                this.SetRootViewModel<HomeViewModel>();
            }
            else
            {
                // Redirect user to main UI
                this.SetRootViewModel<DrawerViewModel>();
            }

            // Push notification registration
            if (appSettings.EnablePushNotification && appSettings.PushRegistrationMode == PushRegistrationMode.OnAppStart)
            {
                var pushRegistrationService = this.GetService<IPushRegistrationService>();
                pushRegistrationService.RegisterPushNotificationAsync();
            }
        }

        #endregion
    }
}