using System.Web.Mvc;
using System.Web.Routing;
using Core.Code.ActionResults;

namespace Core.Code.ResponseCodecs
{
    public interface IResponseCodec
    {
        bool CanExecute(string[] acceptTypes, RouteData routeData);
        ActionResult Execute(RouteData routeData, ResultHandledByCodecResult resultResult);
    }
}