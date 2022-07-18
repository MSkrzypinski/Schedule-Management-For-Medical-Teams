using Domain.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Infrastructure.Coordinators;
using Infrastructure.MedicalTeams;
using Infrastructure.MedicalWorkerProfessions;
using Infrastructure.MedicalWorkerProfessionsToPermission;
using Infrastructure.MedicalWorkers;
using Infrastructure.Schedules;
using Infrastructure.Shifts;
using Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class ScheduleManagementContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Coordinator> Coordinators { get; set; }
        public DbSet<MedicalTeam> MedicalTeams { get; set; }
        public DbSet<MedicalWorker> MedicalWorkers { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<MedicalWorkerProfessionsToPermissions> MedicalWorkerProfessionsToPermissions { get; set; }
        public ScheduleManagementContext(DbContextOptions<ScheduleManagementContext> dbContextOptions): base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MedicalTeamEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CoordinatorEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MedicalWorkerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MedicalWorkerProfessionsToPermissionsEntityConfiguration());
            
        }
    }
}
