using Application.Persistence;
using Domain.Entities;
using Infrastructure.Coordinators;
using Infrastructure.MedicalTeams;
using Infrastructure.MedicalWorkers;
using Infrastructure.Schedules;
using Infrastructure.Shifts;
using Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public static class InfrastructureServicesConfiguration
    {
        public static IServiceCollection AddScheduleManagementInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ScheduleManagementContext>(options =>
            {
                options.UseSqlServer(configuration.
                    GetConnectionString("ScheduleManagementConnectionString"));

                options.UseSqlServer(configuration.
                    GetConnectionString("ScheduleManagementConnectionString"),
                    provideroptions =>
                    {
                        provideroptions.CommandTimeout(180);
                    });
            });
            
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICoordinatorRepository, CoordinatorRepository>();
            services.AddScoped<IMedicalTeamRepository,MedicalTeamRepository>();
            services.AddScoped<IMedicalWorkerRepository, MedicalWorkerRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IShiftRepository, ShiftRepository>();
            services.AddScoped(typeof(IGenericCounter<>), typeof(GenericCounter<>));

            return services;
        }
    }
}
