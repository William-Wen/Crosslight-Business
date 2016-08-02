using Business.Infrastructure;
using Intersoft.Crosslight;
using PushNotification = Intersoft.Crosslight.Services.PushNotification.Android;
using Social = Intersoft.Crosslight.Services.Social.Android;

namespace Business.Android.Infrastructure
{
    /// <summary>
    ///     Android's application initializer.
    /// </summary>
    public sealed class AppInitializer : IApplicationInitializer
    {
        #region Methods

        /// <summary>
        ///     Gets the application service.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IApplicationService GetApplicationService(IApplicationContext context)
        {
            return new CrosslightAppAppService(context);
        }

        /// <summary>
        ///     Initializes the application.
        /// </summary>
        /// <param name="appHost">The application host.</param>
        public void InitializeApplication(IApplicationHost appHost)
        {
        }

        /// <summary>
        ///     Initializes the components.
        /// </summary>
        /// <param name="appHost">The application host.</param>
        public void InitializeComponents(IApplicationHost appHost)
        {
        }

        /// <summary>
        ///     Initializes the services.
        /// </summary>
        /// <param name="appHost">The application host.</param>
        public void InitializeServices(IApplicationHost appHost)
        {
            AndroidApp.PreserveAssembly((typeof(PushNotification.ServiceInitializer).Assembly));
            AndroidApp.PreserveAssembly((typeof(Social.ServiceInitializer).Assembly));
        }

        #endregion
    }
}