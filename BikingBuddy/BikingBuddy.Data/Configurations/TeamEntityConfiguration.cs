using BikingBuddy.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BikingBuddy.Data.Configurations;

public class TeamEntityConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {

        builder
            .HasMany(e => e.TeamMembers)
            .WithOne(e => e.Team)
            .HasForeignKey(e => e.TeamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.TeamManager)
            .WithMany(e => e.ManagingTeams)
            .HasForeignKey(e => e.TeamManagerId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}