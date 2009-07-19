// ReSharper disable InconsistentNaming

using System.Web.Mvc;
using GuerillaTactics.Testing;
using GuerillaTactics.Testing.Mvc;
using MyFeed.Core.Domain.Services;
using MyFeed.Presentation.Configuration;
using MyFeed.Presentation.Controllers;
using MyFeed.Presentation.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace specs_for_FeedUpdateController
{
    public abstract class FeedUpdateController_base_context : Specification<FeedUpdateController>
    {
        protected ICurrentUserDetailsProvider the_current_user_details_provider;

        protected override void EstablishContext()
        {
            the_current_user_details_provider= MockRepository.GenerateStub<ICurrentUserDetailsProvider>();
        }

        protected override FeedUpdateController CreateSubject()
        {
            return new FeedUpdateController(the_current_user_details_provider);
        }
    }

    [TestFixture]
    public class when_performing_a_get_request : FeedUpdateController_base_context
    {
        private ActionResult the_result;

        protected override void When()
        {
            the_result = Subject.Index();
        }

        [Test]
        public void the_form_view_should_be_rendered()
        {
            the_result.should_render_default_view_for_action();
        }
    }

    [TestFixture]
    public class when_performing_a_post_request_and_the_data_is_invalid : FeedUpdateController_base_context
    {
        private ActionResult the_result;
        private FeedUpdateViewModel the_view_model;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            the_view_model = new FeedUpdateViewModel();
        }

        protected override void When()
        {
            Subject.ModelState.AddModelError("Content", "some error message");
            the_result = Subject.Index(the_view_model);
        }

        [Test]
        public void the_form_view_should_be_rendered()
        {
            the_result.should_render_default_view_for_action();
        }

        [Test]
        public void the_users_entered_information_should_be_rerendered_on_the_view()
        {
            the_result.As<ViewResult>().ViewData.Model.should_be_the_same_as(the_view_model);
        }
    }

    [TestFixture]
    public class when_performing_a_post_request_and_the_data_is_valid : FeedUpdateController_base_context
    {
        private FeedUpdateViewModel the_view_model;
        private ActionResult the_result;
        private string the_logged_in_users_username;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            the_logged_in_users_username = "mike";
            the_view_model = new FeedUpdateViewModel();
            the_current_user_details_provider.Stub(p => p.GetUsername()).Return(the_logged_in_users_username);
        }

        protected override void When()
        {
            the_result = Subject.Index(the_view_model);
        }

        [Test]
        public void the_user_should_be_redirected_to_their_feed_page()
        {
            the_result.should_redirect_to_route(MyFeedRoutingConfiguration.ViewFeedRoute)
                .with_parameters(new
                                    {
                                        username = the_logged_in_users_username
                                    });
        }
    }
} ;