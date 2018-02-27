using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CommonHelpers.Helpers;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("program starting...");
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var rootDir = ServerHelper.GetRootDirectory();
            //rootDir = "/root/";
            var webhost = WebHost.CreateDefaultBuilder(args)
                 .UseUrls(GetListenUrls())//修改默认端口
                 .UseStartup<Startup>()
                 .ConfigureAppConfiguration((hostContext, config) =>
                 {
                     // delete all default configuration providers
                     config.Sources.Clear();
                     config.SetBasePath(rootDir);
                     config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                     config.AddJsonFile($"appsettings.Development.json", optional: true);
                 })
                 .Build();

            return webhost;
        }

        /// <summary>
        /// 获取监听url列表
        /// </summary>
        /// <returns></returns>
        private static string[] GetListenUrls()
        {
            var configuration = BuildConfiguration();
            List<string> urls = new List<string>();
            var list = configuration.GetSection("SiteConfig").GetSection("ListenUrls").GetChildren();
            foreach (var url in list)
            {
                urls.Add(url.Value);
            }

            return urls.ToArray();
        }

        /// <summary>
        /// 构建配置对象
        /// </summary>
        private static IConfiguration BuildConfiguration()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(ServerHelper.GetRootDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile("appsettings.Development.json", optional: true)
                 .Build();

            return configuration;
        }
    }
}
