using BikingBuddy.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BikingBuddy.Data.Configurations;

public class TeamEntityConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder
            .HasOne(t => t.TeamManager)
            .WithOne(t => t.Team)
            .OnDelete(DeleteBehavior.Restrict);

    }
}