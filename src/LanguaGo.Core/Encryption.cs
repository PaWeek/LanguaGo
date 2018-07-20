using System.Security.Cryptography;
using System.Text;

namespace LanguaGo.Core
{
    public static class Encryption
    {
        public static string Sha1encrypt(string phrase)
        {
            string salt = "Random";
            UTF8Encoding encoder = new UTF8Encoding();
            SHA1CryptoServiceProvider sha1hasher = new SHA1CryptoServiceProvider();
            byte[] hashedDataBytes = sha1hasher.ComputeHash(encoder.GetBytes(phrase+salt));
            return ByteArrayToString(hashedDataBytes);
        }

        public static string ByteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        } 
    }
}