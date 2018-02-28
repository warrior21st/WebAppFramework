using OtpSharp;
using System;
using System.Security.Cryptography;
using System.Text;
using Wiry.Base32;

namespace GoogleAuthenticator
{
    /// <summary>
    /// 谷歌认证器帮助类
    /// </summary>
    public static class GoogleAuthenticatorHelper
    {
        /// <summary>
        /// 获取认证器二维码url
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="userName"></param>
        /// <param name="programName"></param>
        /// <returns></returns>
        public static string GetAuthenticatorUrl(string secretKey, string userName, string programName)
        {
            var barcodeUrl = string.Empty;
            var secretKeyBytes = GetGoogleAuthenticatorSecretKeyBytes(secretKey);
            barcodeUrl = $"{KeyUrl.GetTotpUrl(secretKeyBytes, userName)}&issuer={programName}";

            return barcodeUrl;
        }

        /// <summary>
        /// 获取google认证器secretkey-bytes
        /// </summary>
        /// <param name="stringSecretKey"></param>
        /// <returns></returns>
        private static byte[] GetGoogleAuthenticatorSecretKeyBytes(string stringSecretKey)
        {
            return Base32Encoding.Standard.ToBytes(stringSecretKey);
        }

        /// <summary>
        /// 生成新的google认证器secretkey
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateNewGoogleAuthenticatorSecretKey(int length = 20)
        {
            var ran = new Random();
            var s= "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var str = "";
            for(int i = 0; i < length; i++)
            {
                str += s[ran.Next(0, s.Length)];
            }

            return str;
            //var secretKey = Base32Encoding.Standard. .GenerateRandomKey(length/2);               
            //return Base32Encoding.Standard.GetString(secretKey);
        }

        /// <summary>
        /// 验证google认证器token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool ValidateGoogleAuthenticatorToken(string secretKey, string token)
        {
            long timeStepMatched = 0;
            var secretKeyBytes = GetGoogleAuthenticatorSecretKeyBytes(secretKey);
            var otp = new Totp(secretKeyBytes);

            bool valid = otp.VerifyTotp(token, out timeStepMatched, new VerificationWindow(2, 2));

            return valid;
        }
    }
}
