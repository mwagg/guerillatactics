namespace MyFeed.Core.Domain.Services
{
    public class FakeCurrentUserDetailsProvider : ICurrentUserDetailsProvider
    {
        public string GetUsername()
        {
            return "mike";
        }
    }
}