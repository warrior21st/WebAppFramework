using Geolocation;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GeoIpServiceExtensions
    {
        /// <summary>
        /// 添加数据访问层，使用efcore
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddGeplocation(this IServiceCollection services, string dbFileUri)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<GeoIpOptions>((x) =>
            {
                var options = new GeoIpOptions();
                options.GeoIpDbFileUri = dbFileUri;

                return options;
            });

            services.AddSingleton<IGeoIp, GeoIp>();

            return services;
        }
    }
}
