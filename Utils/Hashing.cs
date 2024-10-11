using System.Security.Cryptography;
using System.Text;
namespace WPF_Dusza.Utils
{
    public static class Hashing
    {
        public static string HashPassword(string password)
        {
            string computed = "";
            using SHA256 sha = SHA256.Create();
            byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
            computed = BitConverter.ToString(sha.ComputeHash(pwdBytes)).Replace("-","").ToLower();
            return computed;
        }
    }
}
