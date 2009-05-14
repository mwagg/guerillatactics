using System.Web.Mvc;
using Core.Code.ActionResults;
using Core.Code.Filters;
using Core.Code.ResponseCodecs;
using Spike.Models;

namespace Spike.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        [ResponseCanBeHandledBy(typeof(HtmlResponseCodec))]
        [ResponseCanBeHandledBy(typeof(JsonResponseCodec))]
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            var model = new[] {new Person("John"), new Person("Jack"), new Person("Jill")};

            return new ResultHandledByCodecResult(model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}