using System;
using System.Web.Mvc;
using Core.Code.ActionResults;
using Core.Code.ResponseCodecs;
using Microsoft.Practices.ServiceLocation;

namespace Core.Code.Filters
{
    public class ResponseCanBeOverridenByExtensionAttribute : ActionFilterAttribute
    {
        private readonly string _extension;
        private readonly Type _codecType;

        public ResponseCanBeOverridenByExtensionAttribute(string extension, Type codecType)
        {
            _extension = extension;
            _codecType = codecType;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var resourceResult = filterContext.Result as ResultHandledByCodecResult;

            if(resourceResult == null)
            {
                return;
            }

            var extension = (string) filterContext.RouteData.Values["extension"] ?? string.Empty;

            if(extension == _extension)
            {
                var codec = (IResponseCodec) ServiceLocator.Current.GetInstance(_codecType);
                filterContext.Result = codec.Execute(filterContext.RouteData, resourceResult);
            }
        }
    }
}