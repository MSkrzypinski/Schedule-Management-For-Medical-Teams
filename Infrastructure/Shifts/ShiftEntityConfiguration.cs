using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Shifts
{
    public class ShiftEntityConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Shifts","shifts");

            builder.OwnsOne(x => x.DateRange, x =>
               {
                   x.Property(x => x.Start).HasColumnType("smalldatetime").IsRequired();
                   x.Property(x => x.End).HasColumnType("smalldatetime").IsRequired();
               });

            builder.HasOne(x => x.MedicalTeam);

            builder.HasOne(x => x.CrewMember);
            builder.HasOne(x => x.Driver);
            builder.HasOne(x => x.Manager);
            builder.HasMany(x => x.Crew).WithMany(x => x.Shifts);
        }
    }
}
