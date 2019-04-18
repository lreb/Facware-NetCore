using System;
using Facware.Library.Logger.Implementations;
using Facware.Library.Logger.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Facware.Services.Email.Extensions
{
    public static class ServiceExtensions
    {
        /*public ServiceExtensions()
        {
        }*/
        /// <summary>
        /// Configures the cors. CORS (Cross-Origin Resource Sharing)
        /// </summary>
        /// <param name="services">Services.</param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
        /// <summary>
        /// Configures the IISI ntegration. IIS Configuration as Part of .NET Core Configuration
        /// </summary>
        /// <param name="services">Services.</param>
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
