using System.Collections.Generic;
using System.Linq;
using MyFeed.Core.Domain.Model;

namespace MyFeed.Core.Domain.Queries
{
    public class FindUserByUsernameQuery : IFindUserByUsernameQuery
    {
        private List<User> _users;

        public FindUserByUsernameQuery()
        {
            _users = new List<User> {new User("michael", "password")};
        }

        public IEnumerable<User> GetQuery(string username)
        {
            return _users.Where(u => u.Username == username);
        }
    }
}