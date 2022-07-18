using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Coordinators
{
    public class CoordinatorEntityConfiguration : IEntityTypeConfiguration<Coordinator>
    {
        public void Configure(EntityTypeBuilder<Coordinator> builder)
        {
            builder.ToTable("Coordinators","coordinators");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User);
            builder.HasMany(x => x.MedicalTeams).WithOne(s => s.Coordinator).IsRequired();

        }
    }
}
