using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelpers.Helpers
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 获取文件md5
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static string GetFileMD5(FileStream fs)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取文件后缀，不带.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileExt(string fileName)
        {
            var result = string.Empty;
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                var arr = fileName.Split('.');
                result = arr.Length > 1 ? arr[arr.Length - 1] : result;
            }

            return result;
        }
    }
}
