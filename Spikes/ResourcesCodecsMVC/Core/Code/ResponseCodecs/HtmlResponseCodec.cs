using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Core.Code.ActionResults;

namespace Core.Code.ResponseCodecs
{
    public class HtmlResponseCodec : IResponseCodec
    {
        public bool CanExecute(string[] acceptTypes)
        {
            return acceptTypes.Contains("text/html");
        }

        public ActionResult Execute(RouteData routeData, ResultHandledByCodecResult resultResult)
        {
            var viewName = (string) routeData.Values["action"];

            var result = new ViewResult();
            result.ViewName = viewName;
            result.ViewData.Model = resultResult.Resource;

            return result;
        }
    }
}