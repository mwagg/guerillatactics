using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;
using MyFeed.Core.Infrastructure.MVC.Results;

namespace MyFeed.Core.Infrastructure.MVC.Codecs
{
    public class HtmlResponseCodec : IResponseCodec
    {
        public bool CanExecute(string[] acceptTypes, RouteData routeData)
        {
            var regex = new Regex("^text/html");
            foreach (var acceptType in acceptTypes)
            {
                if (regex.IsMatch(acceptType))
                {
                    return true;
                }
            }

            return false;
        }

        public ActionResult Execute(ActionExecutedContext context, ResourceResult resourceResult)
        {
            var viewName = (string) context.RouteData.Values["action"];

            var result = new ViewResult();
            result.ViewName = viewName;
            result.ViewData = context.Controller.ViewData;
            result.ViewData.Model = resourceResult.Data;

            return result;
        }
    }
}