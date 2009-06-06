using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using Castle.Components.Validator;
using GuerillaTactics.Common.Utility;
using GuerillaTactics.Testing;
using MyFeed.Presentation.Models;
using MyFeed.Presentation.Models.Binders;
using NUnit.Framework;

namespace specs_for_PostNewFeedUpdateModelBinder
{
    public abstract class base_context : Specification<PostNewFeedUpdateModelBinders>
    {
        private static IValidatorRegistry validationRegistry = new CachedValidationRegistry();
        protected ModelBindingContext binding_context;
        protected ControllerContext controller_context;
        private DateTime system_time;
        private DisposableAction systemTimeOverride;
        protected string username;
        protected string content;
        protected object model;

        protected PostNewFeedUpdateModel TypedModel
        {
            get { return model as PostNewFeedUpdateModel; }
        }

        protected override PostNewFeedUpdateModelBinders CreateSubject()
        {
            return new PostNewFeedUpdateModelBinders(validationRegistry);
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();

            binding_context = new ModelBindingContext();

            system_time = DateTime.Now;
            systemTimeOverride = new DisposableAction(() => SystemTime.Now = () => system_time,
                                                      SystemTime.ResetDelegate);

            binding_context.ValueProvider = new Dictionary<string, ValueProviderResult>();
            AddRequestValue("username", username);
            AddRequestValue("content", content);
        }

        private void AddRequestValue(string valueKey, string value)
        {
            binding_context.ValueProvider.Add(valueKey, 
                                              new ValueProviderResult(value, value, CultureInfo.CurrentCulture));
        }

        public override void AfterEachSpec()
        {
            base.AfterEachSpec();

            systemTimeOverride.Dispose();
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

        protected override void When()
        {
            model = Subject.BindModel(controller_context, binding_context);
        }

        [Test]
        public void the_returned_model_should_be_a_PostNewFeedUpdateModel()
        {
            model.should_be_instance_of_type<PostNewFeedUpdateModel>();
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

        protected override void When()
        {
            model = Subject.BindModel(controller_context, binding_context);
        }

        [Test]
        public void the_returned_model_should_be_a_PostNewFeedUpdateModel()
        {
            model.should_be_instance_of_type<PostNewFeedUpdateModel>();
        }

        [Test]
        public void the_username_request_value_should_be_set_on_the_model()
        {
            TypedModel.Username.should_be_equal_to(username);
        }

        [Test]
        public void an_error_should_be_added_to_the_model_state_for_the_empty_content()
        {
            binding_context.ModelState[TypedModel.GetPropertyName(m => m.Content)].
                Errors[0].ErrorMessage.should_be_equal_to("Please enter the text of your update.");
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

        protected override void When()
        {
            model = Subject.BindModel(controller_context, binding_context);
        }

        [Test]
        public void the_returned_model_should_be_a_PostNewFeedUpdateModel()
        {
            model.should_be_instance_of_type<PostNewFeedUpdateModel>();
        }

        [Test]
        public void the_username_request_value_should_be_set_on_the_model()
        {
            TypedModel.Username.should_be_equal_to(username);
        }

        [Test]
        public void an_error_should_be_added_to_the_model_state_for_the_content_length()
        {
            binding_context.ModelState[TypedModel.GetPropertyName(m => m.Content)].
                Errors[0].ErrorMessage.should_be_equal_to("Your update cannot be greater than 140 characters in length.");
        }
    }
}