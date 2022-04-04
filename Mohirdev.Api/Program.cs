using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohirdev.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File(
                    path: "Logs/log.txt",
                    outputTemplate: "{Timestamp: yyyy-MM-dd HH:mm:ss } " +
                    "[{Level:u3}] {Message} {NewLine} {Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                ).CreateLogger();
            try
            {
                Log.Information("Dastur yurdi");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Error berdi");
                Log.Warning(exception, "Dastur yurishda xatolik bor");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}