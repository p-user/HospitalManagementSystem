﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Data.Configurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> b)
        {
            b.Property(u => u.UserName).HasMaxLength(256);
            b.Property(u => u.NormalizedUserName).HasMaxLength(256);
            b.Property(u => u.Email).HasMaxLength(256);
            b.Property(u => u.NormalizedEmail).HasMaxLength(256);
            b.HasMany(e => e.Claims)
               .WithOne()
               .HasForeignKey(uc => uc.UserId)
               .IsRequired();

            b.HasMany(e => e.UserRoles)
               .WithOne()
               .HasForeignKey(ur => ur.UserId)
               .IsRequired();



        }
    }
}