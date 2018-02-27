using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelpers.Algorithm
{
    /// <summary>
    /// hash函数
    /// </summary>
    public static class Hash
    {
        public static byte[] GetMd5Byte(string inputString)
        {
            System.Security.Cryptography.HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetMd5(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetMd5Byte(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public static string GetMd5(byte[] bytes)
        {
            var  algorithm   = MD5.Create();  
            var  result      = algorithm.ComputeHash(bytes);
            var  sb          = new StringBuilder();

            foreach (byte b in result)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        } 

        public static string GetSHA1(string text)
        {
            var sha1 = SHA1.Create();
            var inputBytes = Encoding.UTF8.GetBytes(text);
            var hash = sha1.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public static string GetSha256(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = SHA256.Create())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        } 
    }
}
