using Domain.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MedicalTeams
{
    public class MedicalTeamEntityConfiguration : IEntityTypeConfiguration<MedicalTeam>
    {
        public void Configure(EntityTypeBuilder<MedicalTeam> builder)
        {
            builder.ToTable("MedicalTeams","medicalTeams");
            builder.HasKey(x => x.Id);
            
            builder.OwnsOne<InformationAboutTeam>("InformationAboutTeam", x =>
            {
                x.Property(x=>x.Code).HasColumnName("TeamCode").IsRequired().HasMaxLength(15);
                x.Property(x => x.City).HasColumnName("City").IsRequired().HasMaxLength(100);
                x.Property(x=>x.SizeOfTeam).HasColumnName("SizeOfTeam").IsRequired();
                x.Property(x=>x.MedicalTeamType).HasColumnName("MedicalTeamType").HasConversion<string>().IsRequired();
            });
        }
    }
}
