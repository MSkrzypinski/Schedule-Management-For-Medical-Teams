using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Users
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users","users");

            var emailConverter = new ValueConverter<Email, string>(
                v => v.Value,
                v => new Email(v));

            var nameConverter = new ValueConverter<Name, string>(
                v => v.FirstName + " " + v.LastName,
                v => new Name(v.Remove(v.LastIndexOf(' ')), v.Substring(v.LastIndexOf(' ')+1)));

            var passwordConverter = new ValueConverter<Password, string>(
                v => v.Value,
                v => new Password(v));

            var phoneNumberConverter = new ValueConverter<PhoneNumber, string>(
                v => v.Number,
                v => new PhoneNumber(v));

            builder.HasKey(x => x.Id);
            builder.Property<Name>("Name").HasColumnName("Name").HasConversion(nameConverter).IsRequired().HasMaxLength(50);
            builder.Property<Password>("Password").HasColumnName("Password").HasConversion(passwordConverter).IsRequired();
            builder.Property<PhoneNumber>("PhoneNumber").HasColumnName("PhoneNumber").HasConversion(phoneNumberConverter).IsRequired();
            builder.Property<Email>("Email").HasColumnName("Email").HasConversion(emailConverter).IsRequired();
            
            builder.OwnsMany<UserRole>("UserRoles", x =>
            {
                x.WithOwner().HasForeignKey("UserId");
                x.ToTable("UserRoles","users");
                x.Property<Guid>("UserId");
                x.Property<string>("Value").HasColumnName("RoleCode");
                x.HasKey("UserId", "Value");
            });

        }
    }
}
