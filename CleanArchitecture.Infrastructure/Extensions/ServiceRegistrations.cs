
using CleanArchitecture.Data.Entities.Identities;
using CleanArchitecture.Data.IRpositories.IAuth;
using CleanArchitecture.Data.IRpositories.IGenericRepositories;
using CleanArchitecture.Infrastructure.ContextDB;
using CleanArchitecture.Infrastructure.Repositories.Auth;
using CleanArchitecture.Infrastructure.Repositories.GenericRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Extensions
{
    public static class ServiceRegistrations
    {
        public  static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IReadGenericRepository<>), typeof(ReadGenericRepository<>));
            services.AddScoped(typeof(IWriteGenericRepository<>), typeof(WriteGenericRepository<>));
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddIdentity<UserApp, UserApp>().AddEntityFrameworkStores<SchoolProjectDbContext>().AddDefaultTokenProviders();
          // if user enter failed email or password 3 times locked ccount
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;
            });

        }

    }
}
