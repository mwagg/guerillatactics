using GuerillaTactics.Testing;
using MyFeed.Core.Domain.Model;
using MyFeed.Core.Domain.Queries;
using MyFeed.Core.Domain.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace specs_for_UserAuthenticationService
{
    public abstract class base_context : Specification<UserAuthenticationService>
    {
        protected IFindUserByUsernameQuery user_query;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            user_query = MockRepository.GenerateStub<IFindUserByUsernameQuery>();
        }

        protected override UserAuthenticationService CreateSubject()
        {
            return new UserAuthenticationService(user_query);
        }
    }

    public class when_asked_to_authenticate_a_user_who_exists_with_correct_credentials :
        base_context
    {
        private string the_username = "michael";
        private string the_password = "password";
        private bool result;
        private User the_user;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            the_user = new User(the_username, the_password);
            user_query.Stub(q => q.GetQuery(the_username)).Return(new[] {the_user});
        }

        protected override void When()
        {
            result = Subject.AuthenticateUser(the_username, the_password);
        }

        [Test]
        public void authentication_should_be_successful()
        {
            result.should_be_true();
        }
    }
}