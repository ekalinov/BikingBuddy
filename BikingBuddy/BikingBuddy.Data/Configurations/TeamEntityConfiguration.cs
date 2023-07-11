
namespace BikingBuddy.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

    public class TeamEntityConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {

            builder
                .HasMany(e => e.TeamMembers)
                .WithOne(e => e.Team)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}