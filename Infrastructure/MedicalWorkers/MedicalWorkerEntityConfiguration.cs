using Domain.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MedicalWorkers
{
    public class MedicalWorkerEntityConfiguration : IEntityTypeConfiguration<MedicalWorker>
    {
        public void Configure(EntityTypeBuilder<MedicalWorker> builder)
        {
            builder.ToTable("MedicalWorkers","medicalWorkers");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.IsActive);

            builder.OwnsOne(x => x.Address,s=>
            {
                s.Property(x => x.City).HasColumnName("City").IsRequired().HasMaxLength(50);
                s.Property(x => x.ZipCode).HasColumnName("ZipCode").IsRequired().HasMaxLength(6);
                s.Property(x => x.Street).HasColumnName("Street").IsRequired().HasMaxLength(50);
                s.Property(x => x.HouseNumber).HasColumnName("HouseNumber").IsRequired();
                s.Property(x => x.ApartmentNumber).HasColumnName("ApartamentNumber");
            });

            builder.Property(x => x.DateOfBirth).HasColumnName("Date of birth").HasColumnType("Date").IsRequired();
            builder.HasOne(x => x.User);

            builder.HasMany(x => x.Shifts).WithMany(x => x.Crew);

            builder.OwnsMany<DayOff>("DaysOff", x =>
            {
                x.WithOwner().HasForeignKey("MedicalWorkerId");
                x.ToTable("DaysOff","medicalWorkers");
                x.Property<Guid>("MedicalWorkerId");
                x.Property(s => s.Start).HasColumnType("smalldatetime");
                x.Property(s => s.End).HasColumnType("smalldatetime");
                x.HasKey("MedicalWorkerId","Start","End");
            });

            builder.OwnsMany<MedicalWorkerProfession>("MedicalWorkerProfessions", x =>
            {
                x.WithOwner().HasForeignKey("MedicalWorkerId");
                x.ToTable("MedicalWorkerProfessions","medicalWorkers");

                x.Property(x => x.MedicalWorkerProfessionEnum).HasColumnName("MedicalWorkerProfession").HasConversion<string>();
                x.Property<Guid>("MedicalWorkerId");
                x.HasKey("MedicalWorkerProfessionEnum", "MedicalWorkerId");
            });


            builder.OwnsMany<EmploymentContract>("EmploymentContracts", x =>
            {
                x.WithOwner().HasForeignKey("MedicalWorkerId");
                x.ToTable("EmploymentContracts", "medicalWorkers");
                x.HasKey("MedicalWorkerId","ContractType","MedicRole", "MedicalWorkerProfession");
                x.Property(x => x.ContractType).HasConversion<string>();
                x.Property(x => x.MedicRole).HasConversion<string>();
                x.Property(x => x.MedicalWorkerProfession).HasConversion<string>();
                x.HasOne(x => x.MedicalTeam);
            });
        }
    }
   
}
