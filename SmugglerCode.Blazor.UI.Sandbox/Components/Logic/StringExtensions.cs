using System.Security.Cryptography;
using System.Text;

namespace SmugglerCode.Blazor.UI.Sandbox.Components.Logic
{
    public static class StringExtensions
    {
        public static string ToSHA256(this string input)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }
    }
}
