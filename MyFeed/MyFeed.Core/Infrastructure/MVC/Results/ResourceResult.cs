using System.Web.Mvc;

namespace MyFeed.Core.Infrastructure.MVC.Results
{
    public class ResourceResult : ActionResult
    {
        private readonly object _data;

        public ResourceResult(object data)
        {
            _data = data;
        }

        public object Data
        {
            get { return _data; }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 415;
        }
    }
}