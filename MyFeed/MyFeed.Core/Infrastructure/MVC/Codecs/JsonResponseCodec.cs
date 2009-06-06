using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using MyFeed.Core.Infrastructure.MVC.Results;

namespace MyFeed.Core.Infrastructure.MVC.Codecs
{
    public class JsonResponseCodec : IResponseCodec
    {
        public bool CanExecute(string[] acceptTypes, RouteData routeData)
        {
            return acceptTypes.Contains("application/json");
        }

        public ActionResult Execute(ActionExecutedContext context, ResourceResult resourceResult)
        {
            return new JsonResult
                       {
                           Data = resourceResult.Data
                       };
        }
    }
}