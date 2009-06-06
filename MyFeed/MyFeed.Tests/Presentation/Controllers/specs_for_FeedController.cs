//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using GuerillaTactics.Testing;
//using MyFeed.Core.Domain.Commands;
//using MyFeed.Core.Domain.Model;
//using MyFeed.Core.Domain.Queries;
//using MyFeed.Core.Infrastructure;
//using MyFeed.Core.Infrastructure.MVC.Results;
//using MyFeed.Presentation.Controllers;
//using NUnit.Framework;
//using Rhino.Mocks;

//namespace specs_for_FeedController
//{
//    public abstract class FeedController_base_context : Specification<FeedController>
//    {
//        protected IUserFeedUpdateQuery userFeedUpdateQuery;
//        protected IApplicationController applicationController;

//        protected override void EstablishContext()
//        {
//            base.EstablishContext();

//            applicationController = MockRepository.GenerateStub<IApplicationController>();
//            userFeedUpdateQuery = MockRepository.GenerateStub<IUserFeedUpdateQuery>();
//        }

//        protected override FeedController CreateSubject()
//        {
//            return new FeedController(userFeedUpdateQuery, applicationController);
//        }
//    }

//    [TestFixture]
//    public class when_viewing_the_feed_for_an_existing_user_with_updates : FeedController_base_context
//    {
//        private string username = "mike";
//        private ActionResult result;

//        protected override void EstablishContext()
//        {
//            base.EstablishContext();

//            userFeedUpdateQuery.Stub(q => q.Execute(username))
//                .Return(FeedUpdatesObjectMother.FeedsForMike);
//        }

//        protected override void When()
//        {
//            result = Subject.Index(username);
//        }

//        [Test]
//        public void the_feed_updates_for_that_user_should_be_returned()
//        {
//            var resourceResult = result as ResourceResult;
//            var updates = resourceResult.Data as IEnumerable<FeedUpdate>;

//            updates.should_be_equivalent_to(FeedUpdatesObjectMother.FeedsForMike.ToList());
//        }
//    }

//    [TestFixture]
//    public class when_posting_a_new_feed_update : FeedController_base_context
//    {
//        private FeedUpdate feedUpdate;
//        private ActionResult result;

//        protected override void EstablishContext()
//        {
//            base.EstablishContext();

//            feedUpdate = FeedUpdatesObjectMother.FeedsForMike.First();
//        }

//        protected override void When()
//        {
//            result = Subject.Post(feedUpdate);
//        }

//        [Test]
//        public void the_post_feed_command_should_be_executed()
//        {
//            applicationController.AssertWasCalled(
//                ac => ac.ExecuteCommand(Arg<PostFeedUpdateCommand>
//                    .Matches(c => c.FeedUpdate == feedUpdate)));
//        }

//        [Test]
//        public void the_request_is_redirected_to_the_users_feed()
//        {
//            var redirectResult = result as RedirectToRouteResult;
//            redirectResult.RouteValues["controller"].should_be_null();
//            redirectResult.RouteValues["action"].should_be_equal_to("Index");
//            redirectResult.RouteValues["username"].should_be_equal_to(feedUpdate.Username);
//        }
//    }
//}