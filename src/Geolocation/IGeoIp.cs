using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#pragma warning disable 1591
namespace Geolocation
{
    /// <summary>
    /// geoip接口
    /// </summary>
    public interface IGeoIp
    {
        /// <summary>
        /// 根据ip获取位置
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        LocationModel GetLocation(string ip);
    }
}
