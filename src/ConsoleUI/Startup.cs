using System;
using System.IO;
using ConsoleTemplate.Application;
using ConsoleTemplate.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ConsoleTemplate.$safeprojectname$
{
    public class Startup
    {
        private void BuildConfiguration(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
        public void Run(string[] args)
        {
            IHost host;
            var builder = new ConfigurationBuilder();
            BuildConfiguration(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Logger.Information("Application Starting");

                host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) => {
                        services.AddApplication();
                    })
                    .UseSerilog()
                    .Build();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Unable to start the application");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }

            var service = ActivatorUtilities.CreateInstance<PrinterService>(host.Services);
            service.ConsoleOut();
        }
    }
}