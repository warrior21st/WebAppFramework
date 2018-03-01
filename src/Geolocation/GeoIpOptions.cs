using Microsoft.Extensions.Options;
using System;

namespace Geolocation
{
    /// <summary>
    /// geoip选项
    /// </summary>
    public class GeoIpOptions : IOptions<GeoIpOptions>
    {
        /// <summary>
        /// 数据文件uri
        /// </summary>
        public string GeoIpDbFileUri { get; set; }

        GeoIpOptions IOptions<GeoIpOptions>.Value
        {
            get
            {
                return this;
            }
        }
    }
}
