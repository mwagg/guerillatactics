using System.Web.Mvc;
using System.Web.Routing;
using MyFeed.Core.Infrastructure.MVC.Results;

namespace MyFeed.Core.Infrastructure.MVC.Codecs
{
    public interface IResponseCodec
    {
        bool CanExecute(string[] acceptTypes, RouteData routeData);
        ActionResult Execute(ActionExecutedContext context, ResourceResult resultResult);
    }
}