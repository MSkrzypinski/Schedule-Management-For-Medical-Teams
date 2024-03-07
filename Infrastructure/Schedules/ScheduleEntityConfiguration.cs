using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Schedules
{
    public class ScheduleEntityConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Schedules", "schedules");

            builder.HasOne(x => x.MedicalTeam);

            builder.OwnsOne(x => x.MonthAndYearOfSchedule, x =>
               {
                   x.Property(x => x.Month).HasColumnName("Month").IsRequired();
                   x.Property(x => x.Year).HasColumnName("Year").IsRequired();
               });

            builder.Property(x => x.Created).HasColumnName("CreationDate").ValueGeneratedOnAdd();
            builder.HasMany(x => x.Shifts).WithOne(x => x.Schedule).IsRequired();

        }
    }
}
