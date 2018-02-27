using System.Text; 
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System;

namespace CommonHelpers.Algorithm
{
    public class AES
    {
        //盐，自己可以自定义
        private static byte[] saltBytes = new byte[] { 8, 2, 3, 4, 5, 6, 7, 8 };
        /// <summary>
        /// 加密字符串到字节
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="Key"></param> 
        /// <returns></returns>
        public static byte[] EncryptStringToBytes(string plainText, string key)
        {
            byte[] result = null;

            try
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(key);
                // Hash the password with SHA256
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);


                // Create an Aes object
                // with the specified key and IV.
                using (Aes aesAlg = Aes.Create())
                {
                    //加盐
                    var dkey = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    aesAlg.Key = dkey.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = dkey.GetBytes(aesAlg.BlockSize / 8);

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption.
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                            result = msEncrypt.ToArray();
                        }
                    }
                }
            }
            catch
            {

            }

            // Return the encrypted bytes from the memory stream.
            return result;
        }

        /// <summary>
        /// 加密字符串，结果是使用base64编码过的
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String Encrypt(String plainText, String key)
        {
            return Convert.ToBase64String(EncryptStringToBytes(plainText, key));
        }

        /// <summary>
        /// 从字节中解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string DecryptStringFromBytes(byte[] cipherText, String key)
        {
            String result = string.Empty;
            try
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(key);

                // Hash the password with SHA256
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                // Create an Aes object
                // with the specified key and IV.
                using (Aes aesAlg = Aes.Create())
                {
                    //加盐
                    var dkey = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    aesAlg.Key = dkey.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = dkey.GetBytes(aesAlg.BlockSize / 8);

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                }

            }
            catch
            {

            }

            return result;
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string Decrypt(String s, String Key)
        {
            byte[] cipherText = Convert.FromBase64String(s);
            return DecryptStringFromBytes(cipherText, Key);
        }  
    }
}
