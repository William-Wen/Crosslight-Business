using System.Web.Mvc;

namespace Business.WebApi.Controllers
{
    public class HomeController : Controller
    {
        #region Methods

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}