using System;
using System.Web.Mvc;

namespace MyFeed.Core.Infrastructure.MVC.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AwareOfLoggedInUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if(filterContext.HttpContext.Session["CurrentUsername"] != null)
            {
                filterContext.Controller.ViewData["CurrentUsername"] =
                    filterContext.HttpContext.Session["CurrentUsername"];
            }
        }
    }
}