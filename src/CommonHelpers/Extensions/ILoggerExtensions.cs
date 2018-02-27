using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// 日志工具扩展类
    /// </summary>
    public static class ILoggerExtensions
    {
        /// <summary>
        /// 写异常日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ex"></param>
        /// <param name="remark"></param>
        public static void LogError(this ILogger logger, Exception ex, string remark = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append("======================================================================================");
            sb.Append(Environment.NewLine);
            if (!string.IsNullOrWhiteSpace(remark))
            {
                sb.Append($"异常备注：");
                sb.Append(remark);
                sb.Append(Environment.NewLine);
            }
            sb.Append("Message:");
            sb.Append(ex.Message);
            sb.Append(Environment.NewLine);
            sb.Append("Source:");
            sb.Append(ex.Source);
            sb.Append(Environment.NewLine);
            sb.Append("StackTrace:");
            sb.Append(ex.StackTrace);

            logger.LogError(sb.ToString());
        }
    }
}