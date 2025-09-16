using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace CleanArchitecture.Infrastructure.Extensions
{
    public static class LoggerExtension
    {
        public static void AddLoggerApplication(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var day = DateTime.Now.Day;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()

                // Error
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                    .WriteTo.File($"Logs/Error/{year}/{month}/{day}/log-.txt",
                        rollingInterval: RollingInterval.Day))

                // Warning
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                    .WriteTo.File($"Logs/Warning/{year}/{month}/{day}/log-.txt",
                        rollingInterval: RollingInterval.Day))

                // Information
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                    .WriteTo.File($"Logs/Information/{year}/{month}/{day}/log-.txt",
                        rollingInterval: RollingInterval.Day))

                // Debug
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug)
                    .WriteTo.File($"Logs/Debug/{year}/{month}/{day}/log-.txt",
                        rollingInterval: RollingInterval.Day))

                // Verbose
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Verbose)
                    .WriteTo.File($"Logs/Verbose/{year}/{month}/{day}/log-.txt",
                        rollingInterval: RollingInterval.Day))

                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
