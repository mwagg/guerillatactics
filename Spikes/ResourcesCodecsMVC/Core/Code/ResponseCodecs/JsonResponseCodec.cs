using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Core.Code.ActionResults;

namespace Core.Code.ResponseCodecs
{
    public class JsonResponseCodec : IResponseCodec
    {
        public bool CanExecute(string[] acceptTypes)
        {
            return acceptTypes.Contains("application/json");
        }

        public ActionResult Execute(RouteData routeData, ResultHandledByCodecResult resultResult)
        {
            return new JsonResult
                       {
                           Data = resultResult.Resource
                       };
        }
    }
}