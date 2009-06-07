using System;
using System.Collections.Generic;
using MyFeed.Core.Domain.Model;

namespace MyFeed.Core.Domain.Queries
{
    public interface IFindUserByUsernameQuery
    {
        IEnumerable<User> GetQuery(string username);
    }
}