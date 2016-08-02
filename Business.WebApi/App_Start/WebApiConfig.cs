using System.Web.Http;

namespace Business.WebApi
{
    public static class WebApiConfig
    {
        #region Methods

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(name: "DataApi", routeTemplate: "data/{controller}/{action}");

            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new
            {
                id = RouteParameter.Optional
            });
        }

        #endregion
    }
}