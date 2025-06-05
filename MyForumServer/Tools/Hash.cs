using System.Security.Cryptography;
using System.Text;

namespace MyBlockForumServer.Tools
{
    public class Hash
    {
        public static string GetHash(string login, string password)
        {
            string salt1 = "^8{-";
            string salt2 = "&>nm";
            string pass = login + salt1 + password + salt2;
            using (var hash = SHA256.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(pass)).Select(x =>
                x.ToString("X2")));
            }
        }
    }
}
