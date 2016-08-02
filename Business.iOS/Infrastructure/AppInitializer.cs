using Business.Infrastructure;
using Intersoft.Crosslight;
using Intersoft.Crosslight.iOS;
using PushNotification = Intersoft.Crosslight.Services.PushNotification.iOS;

namespace Business.iOS.Infrastructure
{
    /// <summary>
    ///     iOS's application initializer.
    /// </summary>
    /// <seealso cref="Intersoft.Crosslight.IApplicationInitializer" />
    public sealed class AppInitializer : IApplicationInitializer
    {
        #region Methods

        /// <summary>
        ///     Gets the application service based on the current context.
        /// </summary>
        /// <param name="context">The context of the application.</param>
        /// <returns>
        ///     The application service.
        /// </returns>
        public IApplicationService GetApplicationService(IApplicationContext context)
        {
            return new CrosslightAppAppService(context);
        }

        /// <summary>
        ///     Initializes and prepares the application for launch.
        /// </summary>
        /// <param name="appHost">The application host.</param>
        public void InitializeApplication(IApplicationHost appHost)
        {
        }

        /// <summary>
        ///     Initializes the components required for the application to run properly.
        /// </summary>
        /// <param name="appHost">The application host.</param>
        public void InitializeComponents(IApplicationHost appHost)
        {
        }

        /// <summary>
        ///     Initializes the services for the application to start properly.
        /// </summary>
        /// <param name="appHost">The application host.</param>
        public void InitializeServices(IApplicationHost appHost)
        {
            // preserve these assemblies so they are not removed by the mtouch linker
            UIApplicationDelegate.PreserveAssembly((typeof(PushNotification.ServiceInitializer).Assembly));
            UIApplicationDelegate.PreserveAssembly((typeof(Intersoft.Crosslight.Services.Social.iOS.ServiceInitializer).Assembly));
        }

        #endregion
    }
}