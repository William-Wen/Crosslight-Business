using System.Web.Mvc;

namespace Business.WebApi
{
    public class FilterConfig
    {
        #region Methods

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        #endregion
    }
}