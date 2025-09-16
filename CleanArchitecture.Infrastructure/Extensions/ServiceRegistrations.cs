
using CleanArchitecture.Data.Entities.Identities;
using CleanArchitecture.Data.IEmailService;
using CleanArchitecture.Data.IRpositories.IAuth;
using CleanArchitecture.Data.IRpositories.IGenericRepositories;
using CleanArchitecture.Infrastructure.ContextDB;
using CleanArchitecture.Infrastructure.EmailService;
using CleanArchitecture.Infrastructure.Repositories.Auth;
using CleanArchitecture.Infrastructure.Repositories.GenericRepositories;
using CleanArchitecture.Service.Helpers;
using CleanArchitecture.Service.IMangers.IAuthManger;
using CleanArchitecture.Service.Managers.AuthService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Extensions
{
    public static class ServiceRegistrations
    {
        public  static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region GenericRepository
            services.AddScoped(typeof(IReadGenericRepository<>), typeof(ReadGenericRepository<>));
            services.AddScoped(typeof(IWriteGenericRepository<>), typeof(WriteGenericRepository<>));
            #endregion

            #region Regsiter Repository
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthRepository,AuthRepository>();
            #endregion

            #region Add Identity and DbContext
            services.AddIdentity<UserApp, RoleApp>().AddEntityFrameworkStores<SchoolProjectDbContext>().AddDefaultTokenProviders();
            services.AddDbContext<SchoolProjectDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("Connection")));
            #endregion

            #region Add Service
            services.AddScoped<AuthJwt>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailSender, EmailSender>();
            #endregion

            #region Email Configuration
            var emailSettings = new EmailSettings();
            configuration.GetSection("EmailSettings").Bind(emailSettings);

            services.AddSingleton(emailSettings);
            #endregion
        }

    }
}
