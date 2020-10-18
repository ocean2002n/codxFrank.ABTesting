using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;

namespace codxFrank.ABTesting.Services
{
    public class Program
    {
        private static string _envName;
        public static void Main(string[] args)
        {
            #region Serilog
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{_envName}.json", optional: true, reloadOnChange: true)
            .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", typeof(Program).Assembly.GetName().Name)
                //.Enrich.WithProperty("Environment", _envName)
                .CreateLogger();
            #endregion

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, config) =>
                {
                    config.ClearProviders();
                    _envName = hostingContext.HostingEnvironment.EnvironmentName;
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .CaptureStartupErrors(true);
                }).UseSerilog();
    }
}
