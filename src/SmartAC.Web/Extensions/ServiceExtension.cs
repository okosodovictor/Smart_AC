using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartAC.Domain.Interface.Managers;
using SmartAC.Domain.Interface.Repository;
using SmartAC.Domain.Interface.Services;
using SmartAC.Domain.Managers;
using SmartAC.Domain.Services;
using SmartAC.Infrastructure.Repositories;
using SmartAC.Web.Interface.Services;
using SmartAC.Web.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Web.Extensions
{
    public static class ServiceExtension
    {
        public static void AddAppAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Add Managers
            services.AddScoped<IDeviceManager, DeviceManager>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<INotificationManager, NotificationManager>();

            //Add Repositories
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            //Add Services
            var jwtOptions = configuration.GetSection("Jwt").Get<TokenService.Options>();
            services.AddScoped<ITokenService, TokenService>(_ => new TokenService(jwtOptions));
            services.AddScoped<IPasswordHasher, MD5PasswordHasher>();
            services.AddScoped<IAlertingService, AlertingService>();
        }
        public static void AddSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Smart AC API", Version = "v1" });
                c.CustomSchemaIds((type) => type.IsNested ? type.FullName : type.Name);

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };

                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    { securityDefinition, Array.Empty<string>()},
                };

                c.AddSecurityRequirement(securityRequirements);
            });
        }
    }
}
