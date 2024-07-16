using System.Security.Cryptography;
using System.Text;

namespace Application.Utilities
{
    public static class HashHelper
    {
        public static string HmacSHA256(string inputData, string key)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] messageByte = Encoding.UTF8.GetBytes(inputData);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashMessage = hmacsha256.ComputeHash(messageByte);
                string hex = BitConverter.ToString(hashMessage);
                hex = hex.Replace("-", "").ToLower();
                return hex;
            }
        }

        public static string EncodeToBase64(string plainString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainString);
            return Convert.ToBase64String(bytes);
        }

        public static string DecodeFromBase64(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
