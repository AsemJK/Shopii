using Blazored.LocalStorage;
using Coravel;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Shopii.App.Application.Database;
using Shopii.App.Application.Events.Listeners;
using Shopii.App.Application.Jobs;
using Shopii.App.Application.Models;
using Shopii.App.Application.Services;
using Shopii.App.Application.Services.Auth;
using Spark.Library.Auth;
using Spark.Library.Database;
using Spark.Library.Logging;
using Spark.Library.Mail;
using Vite.AspNetCore.Extensions;

namespace Shopii.App.Application.Startup
{
    public static class AppServiceRegistration
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddCustomServices();
            services.AddViteServices();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddDatabase<ShopiDbContext>(config);
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddLogger(config);
            services.AddAuthorization(config, new string[] { CustomRoles.Admin, CustomRoles.User });
            services.AddAuthentication<IAuthValidator>(config);
            services.AddScoped<AuthenticationStateProvider, SparkAuthenticationStateProvider>();
            services.AddJobServices();
            services.AddScheduler();
            services.AddQueue();
            services.AddEventServices();
            services.AddEvents();
            services.AddMailer(config);
            services.AddMudServices();
            services.AddBlazoredLocalStorage();

            return services;
        }

        private static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // add custom services
            services.AddScoped<UsersService>();
            services.AddScoped<RolesService>();
            services.AddScoped<IAuthValidator, AuthValidator>();
            services.AddScoped<AuthService>();
            services.AddScoped<ProductService>();
            return services;
        }

        private static IServiceCollection AddEventServices(this IServiceCollection services)
        {
            // add custom events here
            services.AddTransient<EmailNewUser>();
            return services;
        }

        private static IServiceCollection AddJobServices(this IServiceCollection services)
        {
            // add custom background tasks here
            services.AddTransient<ExampleJob>();
            return services;
        }
    }
}
