using BikingBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikingBuddy.Data.Configurations
{
    internal class UserActivityEntityConfiguration : IEntityTypeConfiguration<UserActivity>
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            builder
                .HasKey(pk => new { pk.ActivityId, pk.AppUserId });

            builder
                .HasOne(e => e.AppUser)
                .WithMany(e => e.UserActivities)
                .HasForeignKey(e => e.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Activity)
                .WithMany(e => e.UserActivities)
                .HasForeignKey(e => e.ActivityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
