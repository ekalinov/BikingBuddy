using BikingBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace BikingBuddy.Data.Configurations
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<AppUser>

    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
                .HasOne(e => e.Town)
                .WithMany(e => e.TownUsers)
                .HasForeignKey(e => e.TownId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Country)
                .WithMany(e => e.CountryUsers)
                .HasForeignKey(e => e.CountryId)
                .OnDelete(DeleteBehavior.Restrict);



            builder
                .Property(e => e.TownId)
                .IsRequired(false);

            builder
                .Property(e => e.CountryId)
                .IsRequired(false);

        }
    }
}
