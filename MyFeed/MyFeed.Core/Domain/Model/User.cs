using System;
using System.Security.Cryptography;
using System.Web.Security;

namespace MyFeed.Core.Domain.Model
{
    public class User
    {
        private readonly string _username;
        private readonly string _salt;
        private string _hashedPassword;

        public User(string username, string password)
        {
            _username = username;
            _salt = GenerateSalt();
            _hashedPassword = GeneratedHashedPassword(password, _salt);
        }

        private string GenerateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[50];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        public string Username
        {
            get { return _username; }
        }

        public string Salt
        {
            get { return _salt; }
        }

        public string HashedPassword
        {
            get { return _hashedPassword; }
        }

        public bool IsPasswordCorrect(string password)
        {
            return GeneratedHashedPassword(password, _salt) == _hashedPassword;
        }

        public static string GeneratedHashedPassword(string password, string salt)
        {
            string saltAndPwd = String.Concat(password, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(
                saltAndPwd, "sha1");

            return hashedPwd;
        }
    }
}