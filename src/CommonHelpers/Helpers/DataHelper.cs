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
        /// <summary>
        /// 保护数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="protectKey"></param>
        /// <param name="encodingType"></param>
        /// <returns></returns>
        static public String Protect(String data, string protectKey, EncodingTypes encodingType = EncodingTypes.Base64)
        {
            return AES.Encrypt(data, protectKey, encodingType);
        }

        /// <summary>
        /// 恢复数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="protectKey"></param>
        /// <param name="encodingType"></param>
        /// <returns></returns>
        static public String UnProtect(String data, string protectKey, EncodingTypes encodingType = EncodingTypes.Base64)
        {
            return AES.Decrypt(data, protectKey, encodingType);
        }
    }
}