using CommonHelpers.Helpers;
using MaxMind.GeoIP2;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

#pragma warning disable 1591
namespace Geolocation
{
    /// <summary>
    /// geoip实现
    /// </summary>
    public class GeoIp : IGeoIp
    {
        private readonly GeoIpOptions _options;
        private static DatabaseReader _reader;

        public GeoIp(GeoIpOptions options)
        {
            _options = options;

            if (_reader == null)
            {
                var reader = new DatabaseReader(ServerHelper.MapPath(options.GeoIpDbFileUri));
                _reader = reader;
            }
        }

        public LocationModel GetLocation(string ip)
        {
            var result = new LocationModel();
            if (!string.IsNullOrWhiteSpace(ip))
            {
                if (_reader.TryCity(ip, out var location))
                {
                    result.City = location.City.Names.ContainsKey("zh-CN") ? location.City.Names["zh-CN"] : location.City.Name;
                    result.Country = location.Country.Names.ContainsKey("zh-CN") ? location.Country.Names["zh-CN"] : location.Country.Name;
                }
            }

            return result;
        }
    }
}
