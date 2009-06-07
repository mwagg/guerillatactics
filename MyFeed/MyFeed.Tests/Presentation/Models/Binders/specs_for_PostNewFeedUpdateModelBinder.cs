using System;
using System.Text;
using GuerillaTactics.Common.Utility;
using GuerillaTactics.Testing;
using MyFeed.Presentation.Models;
using MyFeed.Presentation.Models.Binders;
using MyFeed.Tests.Presentation.Models.Binders;
using NUnit.Framework;

namespace specs_for_PostNewFeedUpdateModelBinder
{
    public abstract class base_context : BindingModelBaseContext<PostNewFeedUpdateModelBinder,
        PostNewFeedUpdateModel>
    {
        private DateTime system_time;
        private DisposableAction systemTimeOverride;
        protected string username;
        protected string content;

        protected override PostNewFeedUpdateModelBinder CreateSubject()
        {
            return new PostNewFeedUpdateModelBinder(validationRegistry);
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();

            system_time = DateTime.Now;
            systemTimeOverride = new DisposableAction(() => SystemTime.Now = () => system_time,
                                                      SystemTime.ResetDelegate);

            AddRequestValue(m => m.Username, username);
            AddRequestValue(m => m.Content, content);
        }

        public override void AfterEachSpec()
        {
            base.AfterEachSpec();

            systemTimeOverride.Dispose();
        }

        [Test]
        public void the_returned_model_should_be_a_PostNewFeedUpdateModel()
        {
            TypedModel.should_not_be_null();
        }
    }

    [TestFixture]
    public class when_binding_with_valid_request_values : base_context
    {
        protected override void EstablishContext()
        {
            username = "michael";
            content = "some content";

            base.EstablishContext();
        }

        [Test]
        public void the_username_request_value_should_be_set_on_the_model()
        {
            TypedModel.Username.should_be_equal_to(username);
        }

        [Test]
        public void the_content_request_value_should_be_set_on_the_model()
        {
            TypedModel.Content.should_be_equal_to(content);
        }

        [Test]
        public void the_published_date_should_be_the_datetime_the_request_was_recieved()
        {
            TypedModel.PublishedDate.should_be_equal_to(SystemTime.Now());
        }
    }

    [TestFixture]
    public class when_binding_with_an_empty_content_request_value : base_context
    {
        protected override void EstablishContext()
        {
            username = "michael";
            content = string.Empty;

            base.EstablishContext();
        }

        [Test]
        public void the_username_request_value_should_be_set_on_the_model()
        {
            TypedModel.Username.should_be_equal_to(username);
        }

        [Test]
        public void an_error_should_be_added_to_the_model_state_for_the_empty_content()
        {
            ModelState[TypedModel.GetPropertyName(m => m.Content)].
                Errors[0].ErrorMessage.should_be_equal_to("Please enter the text of your update");
        }
    }

    [TestFixture]
    public class when_binding_and_the_content_request_value_is_over_140_characters_in_length:base_context
    {
        protected override void EstablishContext()
        {
            username = "michael";
            var builder = new StringBuilder();
            for (int i = 0; i < 141; i++)
            {
                builder.Append("a");
            }
            content = builder.ToString();

            base.EstablishContext();
        }

        [Test]
        public void the_username_request_value_should_be_set_on_the_model()
        {
            TypedModel.Username.should_be_equal_to(username);
        }

        [Test]
        public void an_error_should_be_added_to_the_model_state_for_the_content_length()
        {
            ModelState[TypedModel.GetPropertyName(m => m.Content)].
                Errors[0].ErrorMessage.should_be_equal_to("Your update cannot be greater than 140 characters in length");
        }
    }
}