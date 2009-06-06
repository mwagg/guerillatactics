using System;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using MyFeed.Core.Infrastructure.MVC.Codecs;
using MyFeed.Core.Infrastructure.MVC.Results;

namespace MyFeed.Core.Infrastructure.MVC.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class ResponseCanBeHandledByAttribute :ActionFilterAttribute
    {
        private readonly Type _codecType;

        public ResponseCanBeHandledByAttribute(Type codecType)
        {
            _codecType = codecType;
            if (typeof(IResponseCodec).IsAssignableFrom(codecType) == false)
            {
                throw new ArgumentException("Type must implement IResponseCodec.");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var result = filterContext.Result as ResourceResult;

            if (result == null)
            {
                return;
            }

            var codec = (IResponseCodec) ServiceLocator.Current.GetInstance(_codecType);

            if (codec.CanExecute(filterContext.RequestContext.HttpContext.Request.AcceptTypes,
                                 filterContext.RouteData))
            {
                filterContext.Result = codec.Execute(filterContext, result);
            }
        }
    }
}