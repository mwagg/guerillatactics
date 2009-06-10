using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GuerillaTactics.Testing;
using MyFeed.Core.Infrastructure.MVC.Filters;
using NUnit.Framework;
using Rhino.Mocks;

namespace specs_for_AwareOfLoggedInUserAttribute
{
    public abstract class base_context : Specification<AwareOfLoggedInUserAttribute>
    {
        protected ActionExecutedContext filter_context;
        private ControllerContext controller_context;
        private ActionDescriptor action_descriptor;
        protected HttpContextBase http_context_base;
        protected Controller controller;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            controller = MockRepository.GenerateStub<Controller>();
            controller.ViewData = new ViewDataDictionary();

            http_context_base = MockRepository.GenerateStub<HttpContextBase>();
            http_context_base.Stub(c => c.Session)
                .Return(MockRepository.GenerateStub<HttpSessionStateBase>());

            controller_context = new ControllerContext(http_context_base, new RouteData(),
                controller);

            action_descriptor = MockRepository.GenerateStub<ActionDescriptor>();

            filter_context = new ActionExecutedContext(controller_context, action_descriptor,
                false, null);
        }

        protected override AwareOfLoggedInUserAttribute CreateSubject()
        {
            return new AwareOfLoggedInUserAttribute();
        }
    }

    [TestFixture]
    public class given_no_user_is_logged_in : base_context
    {
        protected override void When()
        {
            Subject.OnActionExecuted(filter_context);
        }

        [Test]
        public void no_username_should_be_set_in_the_viewdata()
        {
             controller.ViewData["CurrentUsername"].should_be_null();
        }
    }

    [TestFixture]
    public class given_a_user_is_logged_in : base_context
    {
        private string username = "michael";

        protected override void EstablishContext()
        {
            base.EstablishContext();

            http_context_base.Session["CurrentUsername"] = username;
        }

        protected override void When()
        {
            Subject.OnActionExecuted(filter_context);
        }

        [Test]
        public void the_current_username_should_be_set_in_the_viewdata()
        {
            controller.ViewData["CurrentUsername"].should_be_equal_to(username);
        }
    }
}