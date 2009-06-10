using System.Web.Mvc;
using System.Web.Routing;
using GuerillaTactics.Testing;
using GuerillaTactics.Testing.Mvc;
using MyFeed.Core.Domain.Services;
using MyFeed.Presentation;
using MyFeed.Presentation.Controllers;
using MyFeed.Presentation.Models.Login;
using NUnit.Framework;
using Rhino.Mocks;

namespace specs_for_LoginController
{
    public abstract class base_context : Specification<LoginController>
    {
        protected IUserAuthenticationService the_authentication_service;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            the_authentication_service = MockRepository.GenerateStub<IUserAuthenticationService>();
        }

        protected override LoginController CreateSubject()
        {
            var controller = new LoginController(the_authentication_service);
            controller.CreateStubContext(new RouteData());
            return controller;
        }
    }

    [TestFixture]
    public class when_requesting_the_login_action_with_the_GET_verb : base_context
    {
        private ActionResult result;

        protected override void When()
        {
            result = Subject.Index();
        }

        [Test]
        public void the_login_view_should_be_displayed()
        {
            result.should_render_default_view_for_action();
        }
    }

    [TestFixture]
    public class when_requesting_the_login_action_with_the_POST_verb_and_credentials_are_correct : base_context
    {
        private ActionResult result;
        private UserCredentials the_user_credentials;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            the_user_credentials = new UserCredentials
                                       {
                                           Username = "michael",
                                           Password = "password"
                                       };
            the_authentication_service.Stub(s => s.AuthenticateUser(the_user_credentials.Username,
                                                                    the_user_credentials.Password))
                .Return(true);
        }

        protected override void When()
        {
            result = Subject.Index(the_user_credentials);
        }

        [Test]
        public void the_client_should_be_redirected_to_the_homepage()
        {
            result.should_redirect_to_route("HomePage");
        }

        [Test]
        public void the_current_username_should_be_stored_in_the_session()
        {
            Subject.Session["CurrentUsername"].should_be_equal_to(the_user_credentials.Username);
        }
    }

    [TestFixture]
    public class when_requesting_the_login_action_with_the_POST_verb_and_credentials_are_incorrect : base_context
    {
        private UserCredentials the_user_credentials;
        private ActionResult result;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            the_user_credentials = new UserCredentials
                                       {
                                           Username = "username",
                                           Password = "passsword"
                                       };
            the_authentication_service.Stub(s => s.AuthenticateUser(the_user_credentials.Username,
                                                                    the_user_credentials.Password)).Return(false);
        }

        protected override void When()
        {
            result = Subject.Index(the_user_credentials);
        }

        [Test]
        public void the_Index_view_should_be_rendered()
        {
            result.should_render_default_view_for_action();
        }

        [Test]
        public void the_entered_user_credentials_should_be_passed_to_the_view()
        {
            result.As<ViewResult>().ViewData.Model.should_be_the_same_as(the_user_credentials);
        }

        [Test]
        public void the_password_should_be_cleared_so_it_is_not_sent_back_with_the_page()
        {
            the_user_credentials.Password.should_be_empty();
        }

        [Test]
        public void an_error_should_be_added_to_the_model_state()
        {
            Subject.ModelState["user-credentials"].Errors[0].ErrorMessage.should_be_equal_to(
                "Username or password incorrect. Please try again");
        }
    }

    [TestFixture]
    public class when_requesting_the_login_action_with_the_POST_verb_and_ther_are_validation_errors : base_context
    {
        private ActionResult result;
        private UserCredentials the_user_credentials;

        protected override void When()
        {
            the_user_credentials = new UserCredentials {Username = "michael", Password = "password"};
            Subject.ModelState.AddModelError("Username", "some error");
            result = Subject.Index(the_user_credentials);
        }

        [Test]
        public void the_Index_view_should_be_rendered()
        {
            result.should_render_default_view_for_action();
        }

        [Test]
        public void the_entered_user_credentials_should_be_passed_to_the_view()
        {
            result.As<ViewResult>().ViewData.Model.should_be_the_same_as(the_user_credentials);
        }

        [Test]
        public void the_password_should_be_cleared_so_it_is_not_sent_back_with_the_page()
        {
            the_user_credentials.Password.should_be_empty();
        }
    }

    [TestFixture]
    public class when_requesting_the_logout_action : base_context
    {
        private ActionResult result;

        protected override void When()
        {
            Subject.Session["CurrentUsername"] = "michael";
            result = Subject.Logout();
        }

        [Test]
        public void the_user_should_be_redirect_to_the_home_page()
        {
            result.should_redirect_to_route(Routes.HomePage);
        }

        [Test]
        public void the_current_username_should_be_removed_from_the_session()
        {
            Subject.Session["CurrentUsername"].should_be_null();
        }
    }
}