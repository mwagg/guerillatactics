using System.Web.Mvc;

namespace SimpleLogin.Controllers
{
    public class LoginController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(string username, string password)
        {
            if (username == "bob" && password == "password")
            {
                return RedirectToRoute("Home");
            }
            return View();
        }
    }
}