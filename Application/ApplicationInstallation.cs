using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Application.User.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Application.Authorization;

namespace Application
{
    public static class ApplicationInstallation
    {
        public static IServiceCollection AddScheduleManagementApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IPasswordHasher<Domain.Entities.User>, PasswordHasher<Domain.Entities.User>>();
            services.AddScoped<IUserExecusionContextAccessor, UserExecusionContextAccessor>();
            services.AddScoped<IAuthorizationHandler,MustBeCoordinatorForThisTeamRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, UserMustBeThisMedicalWorkerRequirementHandler>();
            services.AddHttpContextAccessor();

            services.Configure<JwtConfig>
               (configuration.GetSection("JwtConfig"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(o =>
               {
                   o.RequireHttpsMetadata = false;
                   o.SaveToken = false;
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidIssuer = configuration["JwtConfig:Issuer"],
                       ValidAudience = configuration["JwtConfig:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Key"]))
                   };
                  
               });
            return services;
        }
    }
}
