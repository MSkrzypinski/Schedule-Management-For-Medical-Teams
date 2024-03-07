using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MedicalWorkerProfessions
{
    public class MedicalWorkerProfessionsToPermissionsEntityConfiguration : IEntityTypeConfiguration<MedicalWorkerProfessionsToPermissions>
    {
        public void Configure(EntityTypeBuilder<MedicalWorkerProfessionsToPermissions> builder)
        {
            builder.ToTable("MedicalWorkerProfessionToPermission","medicalWorker");
            builder.HasKey("MedicalWorkerProfession", "MedicRole", "MedicalTeamType");
            builder.Property(x => x.MedicalWorkerProfession).HasConversion<string>().IsRequired();
            builder.Property(x => x.MedicRole).HasConversion<string>().IsRequired();
            builder.Property(x => x.MedicalTeamType).HasConversion<string>().IsRequired();

            builder.HasData
            (
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.BasicMedic,MedicRole.RegularMedic,MedicalTeamType.T),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.BasicMedic, MedicRole.Driver,MedicalTeamType.P),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.BasicMedic, MedicRole.Driver, MedicalTeamType.T),

                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Paramedic, MedicRole.Driver, MedicalTeamType.T),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Paramedic, MedicRole.Driver, MedicalTeamType.P),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Paramedic, MedicRole.Driver, MedicalTeamType.S),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Paramedic, MedicRole.Driver, MedicalTeamType.N),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Paramedic, MedicRole.Manager, MedicalTeamType.P),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Paramedic, MedicRole.Manager, MedicalTeamType.T),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Paramedic, MedicRole.RegularMedic, MedicalTeamType.S),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Paramedic, MedicRole.RegularMedic, MedicalTeamType.P),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Paramedic, MedicRole.RegularMedic, MedicalTeamType.T),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Paramedic, MedicRole.RegularMedic, MedicalTeamType.N),

                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Nurse, MedicRole.Driver, MedicalTeamType.T),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Nurse, MedicRole.Driver, MedicalTeamType.P),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Nurse, MedicRole.Driver, MedicalTeamType.S),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Nurse, MedicRole.Driver, MedicalTeamType.N),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Nurse, MedicRole.Manager, MedicalTeamType.P),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Nurse, MedicRole.Manager, MedicalTeamType.T),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Nurse, MedicRole.RegularMedic, MedicalTeamType.S),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Nurse, MedicRole.RegularMedic, MedicalTeamType.P),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Nurse, MedicRole.RegularMedic, MedicalTeamType.T),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Nurse, MedicRole.RegularMedic, MedicalTeamType.N),

                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Doctor, MedicRole.Driver, MedicalTeamType.T),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Doctor, MedicRole.Driver, MedicalTeamType.P),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Doctor, MedicRole.Driver, MedicalTeamType.S),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Doctor, MedicRole.Driver, MedicalTeamType.N),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Doctor, MedicRole.Manager, MedicalTeamType.P),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Doctor, MedicRole.Manager, MedicalTeamType.T),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Doctor, MedicRole.Manager, MedicalTeamType.S),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Doctor, MedicRole.Manager, MedicalTeamType.N),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Doctor, MedicRole.RegularMedic, MedicalTeamType.S),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Doctor, MedicRole.RegularMedic, MedicalTeamType.P),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Doctor, MedicRole.RegularMedic, MedicalTeamType.T),
                new MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum.Doctor, MedicRole.RegularMedic, MedicalTeamType.N)
            );
        }
    }
}
