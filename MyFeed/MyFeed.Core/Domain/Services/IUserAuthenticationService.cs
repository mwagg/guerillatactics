using System;

namespace MyFeed.Core.Domain.Services
{
    public interface IUserAuthenticationService
    {
        bool AuthenticateUser(string username, string password);
    }
}