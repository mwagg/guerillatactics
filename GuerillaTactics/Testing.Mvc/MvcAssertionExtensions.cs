using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace GuerillaTactics.Testing.Mvc
{
    public static class MvcAssertionExtensions
    {
        public static void should_render_default_view_for_action(this ActionResult result)
        {
            result.should_render_view(string.Empty);
        }

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

        public static RedirectionAssertionHelper should_redirect_to_route(this ActionResult result, string routeName)
        {
            var redirectResult = result.As<RedirectToRouteResult>();
            redirectResult.RouteName.should_be_equal_to(routeName);

            return new RedirectionAssertionHelper(redirectResult);
        }

        public static void should_redirect_to_controller<TController>(this ActionResult result) where TController : IController
        {
            var redirectResult = result.As<RedirectToRouteResult>();
            string controllerName =
                typeof (TController).Name.Substring(0, typeof (TController).Name.Length - "Controller".Length);
            redirectResult.RouteValues["controller"].should_be_equal_to(controllerName);
        }
    }

    public class RedirectionAssertionHelper
    {
        private readonly RedirectToRouteResult _redirectResult;

        public RedirectionAssertionHelper(RedirectToRouteResult redirectResult)
        {
            _redirectResult = redirectResult;
        }

        public RedirectionAssertionHelper with_parameters(object parameters)
        {
            var parametersAsDictionary = new RouteValueDictionary(parameters);

            foreach (var key in _redirectResult.RouteValues.Keys)
            {
                _redirectResult.RouteValues[key].should_be_equal_to(parametersAsDictionary[key]);
            }

            return this;
        }
    }
}