using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.Extensions
{
    public static class ApiVersionExtension
    {
        public static void EnableVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(options => {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
              new QueryStringApiVersionReader("api-version"), // ?api-version=1.0
         new HeaderApiVersionReader("X-Version")      // X-Version: 1.0
                         
   );

            }).AddApiExplorer(option => {
                option.GroupNameFormat = "'v'VVV";
                option.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
