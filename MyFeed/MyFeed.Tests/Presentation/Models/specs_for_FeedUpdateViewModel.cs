using System;
using GuerillaTactics.Testing;
using MyFeed.Core.Domain.Model;
using MyFeed.Presentation.Models;
using NUnit.Framework;

namespace specs_for_FeedUpdateViewModel
{
    public abstract class base_context : Specification<FeedUpdateViewModel>
    {
        protected FeedUpdate feed_update;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            feed_update = new FeedUpdate("a user", "some content", DateTime.Now);
        }

        protected override FeedUpdateViewModel CreateSubject()
        {
            return new FeedUpdateViewModel(feed_update);
        }
    }

    [TestFixture]
    public class when_creating_an_instance_to_wrap_an_empty_model : base_context
    {
        protected override bool RethrowExceptionsThrownDuringWhen
        {
            get
            {
                return false;
            }
        }

        protected override void When()
        {
            new FeedUpdateViewModel(null);
        }

        [Test]
        public void an_exception_should_be_thrown()
        {
            ExceptionThrownDuringWhen.should_be_instance_of_type<ArgumentNullException>();
        }
    }

    [TestFixture]
    public class in_general : base_context
    {
        protected override void When()
        {
        }

        [Test]
        public void the_content_property_should_match_the_underlying_model()
        {
            Subject.Content.should_be_equal_to(feed_update.Content);
        }

        [Test]
        public void the_username_property_should_match_the_underlying_model()
        {
            Subject.Username.should_be_equal_to(feed_update.Username);
        }

        [Test]
        public void the_published_date_should_be_a_fulldate_equivalent_of_the_underlying_model()
        {
            Subject.PublishedDate.should_be_equal_to(feed_update.PublishDateTime.ToString("f"));
        }
    }
}