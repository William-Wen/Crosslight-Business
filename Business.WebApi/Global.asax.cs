using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Intersoft.Crosslight.Containers;
using Intersoft.Crosslight.Data;
using Intersoft.Data.WebApi;
using Intersoft.Messaging.PushService;
using Intersoft.Messaging.PushService.Google;

namespace Business.WebApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Nested type: AppDomainInfo

        public class AppDomainInfo : IAppDomainInfo
        {
            #region Properties

            public ApplicationType ApplicationType
            {
                get { return ApplicationType.Server; }
            }

            public ProviderType ProviderType
            {
                get { return ProviderType.IntersoftWebApi; }
            }

            #endregion
        }

        #endregion

        #region Methods

        protected void Application_Start()
        {
            IocContainer.Current.Register<IAppDomainInfo, AppDomainInfo>();
            AreaRegistration.RegisterAllAreas();
            EntityServerConfig.Initialize();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // To use Google push service, please provide Google Server API Key. You can obtain it from http://cloud.google.com/console
            // For more information, see http://developer.intersoftsolutions.com/display/crosslight/Configuring+Server+Push+Notification+Service

            //PushServiceManager.Instance.RegisterAppleService(new ApplePushChannelSettings(new X509Certificate2(Server.MapPath("<insert your Apple private key file>.p12"), "<certificate password>")), "<insert your application bundle ID here>");
            PushServiceManager.Instance.RegisterGcmService(new GcmPushChannelSettings("<insert your GoogleServerApiKey here>"));
            PushServiceManager.Instance.RegisterWindowsPhoneService();
        }

        #endregion
    }
}