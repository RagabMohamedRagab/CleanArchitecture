
using CleanArchitecture.Data.EmailService;
using CleanArchitecture.Data.Entities.Identities;
using CleanArchitecture.Data.IRpositories.IAuth;
using CleanArchitecture.Data.IRpositories.IGenericRepositories;
using CleanArchitecture.Infrastructure.ContextDB;
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
            services.AddScoped(typeof(IReadGenericRepository<>), typeof(ReadGenericRepository<>));
            services.AddScoped(typeof(IWriteGenericRepository<>), typeof(WriteGenericRepository<>));
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddIdentity<UserApp, RoleApp>().AddEntityFrameworkStores<SchoolProjectDbContext>().AddDefaultTokenProviders();
         
            services.AddScoped<AuthJwt>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository,AuthRepository>();

            services.AddDbContext<SchoolProjectDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("Connection")));
            services.AddScoped<IEmailSend, EmailSend>();
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));


        }

    }
}
