using System.Web.Mvc;

namespace Products.Presentation.Controllers
{
    public class AddNewProductController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()
        {
            return View();
        }
    }
}