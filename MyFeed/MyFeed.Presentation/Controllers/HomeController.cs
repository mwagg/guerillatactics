using System.Web.Mvc;
using MyFeed.Core.Infrastructure.MVC.Filters;

namespace MyFeed.Presentation.Controllers
{
    [AwareOfLoggedInUser]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}