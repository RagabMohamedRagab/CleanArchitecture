
using System.Configuration;
using System.Net;
using System.Reflection;
using System.Threading.RateLimiting;
using CleanArchitecture.Data.Entities.Identities;
using CleanArchitecture.Data.Enums;
using CleanArchitecture.Data.IEmailService;
using CleanArchitecture.Data.IRpositories.IAuth;
using CleanArchitecture.Data.IRpositories.IGenericRepositories;

using CleanArchitecture.Infrastructure.ContextDB;
using CleanArchitecture.Infrastructure.EmailService;
using CleanArchitecture.Infrastructure.Repositories.Auth;
using CleanArchitecture.Infrastructure.Repositories.GenericRepositories;
using CleanArchitecture.Service.Dtos;
using CleanArchitecture.Service.Dtos.FireBaseDtos;
using CleanArchitecture.Service.Dtos.GateWay.PaymobGateWay;
using CleanArchitecture.Service.Dtos.GateWay.PayPalGateWay;
using CleanArchitecture.Service.Dtos.GateWay.Stripe;
using CleanArchitecture.Service.ElasticSearch;
using CleanArchitecture.Service.ElasticSearchManager;
using CleanArchitecture.Service.Helpers;
using CleanArchitecture.Service.IMangers.IAuthManger;
using CleanArchitecture.Service.IMangers.IFirebaseManager;
using CleanArchitecture.Service.IMangers.IPaymobManger;
using CleanArchitecture.Service.IMangers.IPayPalManager;
using CleanArchitecture.Service.Managers.AuthService;
using CleanArchitecture.Service.Managers.FirebaseManger;
using CleanArchitecture.Service.Managers.PaymobManger;
using CleanArchitecture.Service.Managers.PayPalManager;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace CleanArchitecture.Infrastructure.Extensions
{
    public static class ServiceRegistrations
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region GenericRepository
            services.AddScoped(typeof(IReadGenericRepository<>), typeof(ReadGenericRepository<>));
            services.AddScoped(typeof(IWriteGenericRepository<>), typeof(WriteGenericRepository<>));
            #endregion

            #region Regsiter Repository
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            #endregion

            #region Add Identity and DbContext
            services.AddIdentity<UserApp, RoleApp>().AddEntityFrameworkStores<SchoolProjectDbContext>().AddDefaultTokenProviders();
            services.AddDbContext<SchoolProjectDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("Connection")));
            #endregion

            #region Add Service
            services.AddScoped<AuthJwt>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IPayPalAuthentication, PayPalAuthentication>();

            #endregion

            #region Email Configuration
            var emailSettings = new EmailSettings();
            configuration.GetSection("EmailSettings").Bind(emailSettings);

            services.AddSingleton(emailSettings);
            #endregion

            #region Stripe
            services.Configure<StripePayment>(configuration.GetSection("StripePayment"));
            #endregion

            #region Paypal
            services.Configure<PaypalPayment>(configuration.GetSection("PaypalPayment"));
            services.AddHttpClient(ClientFactoryKey.PayPal.ToString(), option => {
                option.BaseAddress = new Uri(configuration["PaypalPayment:Url"]!);
            });
            #endregion

            #region Paymob
            services.AddTransient<IAuthenticateManger, AuthenticateManger>();
            services.Configure<Paymob>(configuration.GetSection("Paymob"));
            services.AddHttpClient(ClientFactoryKey.Paymob.ToString(), option => {
                option.BaseAddress = new Uri(configuration["Paymob:Auth"]!);
            });
            #endregion

            #region Meditor
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


            #endregion

            #region Cultures
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options => {
                var supportedCultures = new[] { "en", "ar" };
                options.SetDefaultCulture("en");
                options.AddSupportedCultures(supportedCultures);
                options.AddSupportedUICultures(supportedCultures);
            });


            #endregion

            #region FireBase
            services.Configure<FirebasePushNotifcation>(configuration.GetSection("FirebasePushNotifcation"));
            services.AddHttpClient(ClientFactoryKey.FireBaseAuth.ToString(), option => {
                option.BaseAddress = new Uri(configuration["FirebasePushNotifcation:GlobalScope"]!);
            });
            services.AddScoped<IFirebaseAuthenticate, FirebaseAuthenticate>();
            services.AddHttpClient(ClientFactoryKey.FireBaseSend.ToString(), option => {
                option.BaseAddress = new Uri($"https://fcm.googleapis.com/v1/projects/");
            });

            #endregion

            #region ElasticSearch
            var settings = new ConnectionSettings(new Uri(configuration["ElasticSearch:ElasticUrl"]!));
            var clients = new ElasticClient(settings);
            services.AddSingleton(clients);
            services.AddScoped(typeof(IElasticSearch<>), typeof(ElasticSearch<>));
            #endregion

            #region Redis Cache
            services.AddStackExchangeRedisCache(option => {
                option.Configuration = configuration["Redis:Url"];
            });
            #endregion


            #region RabbitMQ
            services.Configure<RabbitMQConfig>(configuration.GetSection("RabbitMQ"));
            #endregion



            #region Rate Limiting
            // Add rate limiting
           services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;

                // 1️⃣ Fixed Window Limiter
                options.AddFixedWindowLimiter("Fixed", limiterOptions =>
                {
                    limiterOptions.PermitLimit = 5; // max 5 requests
                    limiterOptions.Window = TimeSpan.FromSeconds(10); // per 10 seconds
                    limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    limiterOptions.QueueLimit = 2; // optional: queue 2 extra requests
                });

                // 2️⃣ Sliding Window Limiter
                options.AddSlidingWindowLimiter("Slide", limiterOptions =>
                {
                    limiterOptions.PermitLimit = 5; // max 5 requests
                    limiterOptions.Window = TimeSpan.FromSeconds(10); // sliding 10s window
                    limiterOptions.SegmentsPerWindow = 5; // divide window into 5 segments
                    limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    limiterOptions.QueueLimit = 2;
                });

                // 3️⃣ Token Bucket Limiter
                options.AddTokenBucketLimiter("Token", limiterOptions =>
                {
                    limiterOptions.TokenLimit = 10; // max tokens
                    limiterOptions.TokensPerPeriod = 5; // tokens added each period
                    limiterOptions.ReplenishmentPeriod = TimeSpan.FromSeconds(10); // refill period
                    limiterOptions.AutoReplenishment = true;
                    limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    limiterOptions.QueueLimit = 2;
                });

                // 4️⃣ Concurrency Limiter
                options.AddConcurrencyLimiter("Concurrency", limiterOptions =>
                {
                    limiterOptions.PermitLimit = 2; // max concurrent requests
                    limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    limiterOptions.QueueLimit = 2; // optional: queue extra requests
                });
            });
            #endregion
        }

    }
}
