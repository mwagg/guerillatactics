using System.Linq;
using MyFeed.Core.Domain.Queries;

namespace MyFeed.Core.Domain.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private IFindUserByUsernameQuery _findUserByUsernameQuery;

        public UserAuthenticationService(IFindUserByUsernameQuery findUserByUsernameQuery)
        {
            _findUserByUsernameQuery = findUserByUsernameQuery;
        }

        public bool AuthenticateUser(string username, string password)
        {
            var user = _findUserByUsernameQuery.GetQuery(username)
                .SingleOrDefault();

            return user.IsPasswordCorrect(password);
        }
    }
}