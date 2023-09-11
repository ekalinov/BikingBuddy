
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

            
            builder.Property(e => e.TeamImageUrl)
                .HasDefaultValue("/images/default_team_image.jpg");
        }
    }
}