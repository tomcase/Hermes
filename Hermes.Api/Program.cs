using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Hermes.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Starting Application.");

            try
            {
                var host = Host.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration(ConfigureApplication)
                    .ConfigureWebHostDefaults(ConfigureHost)
                    .Build();

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.Information("Shutting down gracefully...");
                Log.CloseAndFlush();
            }
        }

        private static void ConfigureApplication(IConfigurationBuilder builder)
        {
            builder
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true);
        }

        private static void ConfigureHost(IWebHostBuilder builder)
        {
            builder
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
        }
    }
}