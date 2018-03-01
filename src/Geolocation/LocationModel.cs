using System;
using System.Collections.Generic;
using System.Text;

namespace Geolocation
{
    /// <summary>
    /// 位置模型
    /// </summary>
    public class LocationModel
    {
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 州/省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
    }
}
