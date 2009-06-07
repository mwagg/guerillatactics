using Castle.Components.Validator;

namespace MyFeed.Presentation.Models.Login
{
    public class UserCredentials
    {
        [ValidateNonEmpty("Please enter your username")]
        public string Username { get; set; }

        [ValidateNonEmpty("Please enter your password")]
        public string Password { get; set; }
    }
}