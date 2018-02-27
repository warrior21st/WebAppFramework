using System;
using System.Collections.Generic;
using System.Text;
using CommonHelpers.Algorithm;

namespace CommonHelpers.Helpers
{
    /// <summary>
    /// 数据帮助类，可以保护数据等
    /// </summary>
    public class DataHelper
    {
        private const string PROTECT_KEY = "salt_io_";

        /// <summary>
        /// 保护数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public String Protect(String data)
        {
            return AES.Encrypt(data, PROTECT_KEY);
        }

        /// <summary>
        /// 恢复数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public String UnProtect(String data)
        {
            return AES.Decrypt(data, PROTECT_KEY);
        }

    }
}
