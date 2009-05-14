using System.Web.Mvc;

namespace GuerillaTactics.Testing.Mvc
{
    public static class MvcAssertionExtensions
    {
        public static void should_render_view(this ActionResult result, string viewName)
        {
            var viewResult = result.As<ViewResult>();
            viewResult.ViewName.should_be_equal_to(viewName);
        }

        public static T As<T>(this ActionResult result) where T : ActionResult
        {
            result.should_be_instance_of_type<T>();
            return (T)result;
        }

        public static void should_redirect_to_action(this ActionResult result, string action)
        {
            var redirectResult = result.As<RedirectToRouteResult>();
            redirectResult.RouteValues["action"].should_be_equal_to(action);
        }

        public static void should_redirect_to_controller<TController>(this ActionResult result) where TController : IController
        {
            var redirectResult = result.As<RedirectToRouteResult>();
            string controllerName =
                typeof (TController).Name.Substring(0, typeof (TController).Name.Length - "Controller".Length);
            redirectResult.RouteValues["controller"].should_be_equal_to(controllerName);
        }
    }
}