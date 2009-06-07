using System;
using GuerillaTactics.Common.Utility;
using GuerillaTactics.Testing;
using MyFeed.Presentation.Models.Binders;
using MyFeed.Presentation.Models.Login;
using MyFeed.Tests.Presentation.Models.Binders;
using NUnit.Framework;

namespace specs_for_UserCredentialsModelBinder
{
    public abstract class base_context : BindingModelBaseContext<UserCredentialsModelBinder,
                                             UserCredentials>
    {
        protected string username;
        protected string password;

        protected override UserCredentialsModelBinder CreateSubject()
        {
            return new UserCredentialsModelBinder(validationRegistry);
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();

            AddRequestValue(m => m.Username, username);
            AddRequestValue(m => m.Password, password);
        }

        [Test]
        public void the_returned_model_should_be_a_UserCredentials_object()
        {
            TypedModel.should_not_be_null();
        }
    }

    [TestFixture]
    public class when_the_request_contains_username_and_password_request_values : base_context
    {
        protected override void EstablishContext()
        {
            username = "michael";
            password = "password";

            base.EstablishContext();
        }

        [Test]
        public void the_Username_on_the_model_should_match_the_request_value()
        {
            TypedModel.Username.should_be_equal_to(username);
        }

        [Test]
        public void the_Password_on_the_model_should_match_the_request_value()
        {
            TypedModel.Password.should_be_equal_to(password);
        }
    }

    [TestFixture]
    public class when_the_request_has_no_username : base_context
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();

            username = String.Empty;
            password = "password";
        }

        [Test]
        public void the_Password_on_the_model_should_match_the_request_value()
        {
            TypedModel.Password.should_be_equal_to(password);
        }

        [Test]
        public void an_error_should_be_added_to_the_model_state_for_the_empty_Username()
        {
            ModelState[TypedModel.GetPropertyName(m => m.Username)].
                Errors[0].ErrorMessage.should_be_equal_to("Please enter your username");
        }
    }

    [TestFixture]
    public class when_the_request_has_no_password : base_context
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();

            username = "michael";
            password = String.Empty;
        }

        [Test]
        public void the_Username_on_the_model_should_match_the_request_value()
        {
            TypedModel.Username.should_be_equal_to(username);
        }

        [Test]
        public void an_error_should_be_added_to_the_model_state_for_the_empty_Password()
        {
            ModelState[TypedModel.GetPropertyName(m => m.Password)]
                .Errors[0].ErrorMessage.should_be_equal_to("Please enter your password");
        }
    }
}
