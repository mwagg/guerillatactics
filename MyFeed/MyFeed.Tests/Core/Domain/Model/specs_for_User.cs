using GuerillaTactics.Testing;
using MyFeed.Core.Domain.Model;
using NUnit.Framework;

namespace specs_for_User
{
    public abstract class base_context  :Specification<User>
    {
        public string password;
        protected string username;

        protected override User CreateSubject()
        {
            return new User(username, password);
        }
    }

     [TestFixture]
    public class when_checking_if_a_correct_password_is_correct_for_the_user : base_context
     {
         private bool result;

         protected override void EstablishContext()
         {
             base.EstablishContext();

             username = "michael";
             password = "password";
         }

         protected override void When()
         {
             result = Subject.IsPasswordCorrect(password);
         }

         [Test]
         public void the_result_should_be_true()
         {
             result.should_be_true();
         }
     }

     [TestFixture]
     public class when_checking_if_an_incorrect_password_is_correct_for_the_user : base_context
     {
         private bool result;

         protected override void EstablishContext()
         {
             base.EstablishContext();

             username = "michael";
             password = "password";
         }

         protected override void When()
         {
             result = Subject.IsPasswordCorrect(password + "some junk");
         }

         [Test]
         public void the_result_should_be_false()
         {
             result.should_be_false();
         }
     }

    [TestFixture]
    public class after_a_new_user_is_constructed : base_context
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();

            username = "michael";
            password = "password";
        }

        protected override void When()
        {
        }

        [Test]
        public void the_username_should_be_exposed()
        {
            Subject.Username.should_be_equal_to(username);
        }

        [Test]
        public void a_random_salt_should_be_generated_and_exposed()
        {
            Subject.Salt.should_not_be_null();
        }

        [Test]
        public void the_password_should_be_salted_and_hashed_and_exposed()
        {
            Subject.HashedPassword.should_be_equal_to(
                User.GeneratedHashedPassword(password, Subject.Salt));
        }
    }
}