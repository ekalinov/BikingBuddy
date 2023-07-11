namespace BikingBuddy.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    public class TeamRequestEntityConfiguration : IEntityTypeConfiguration<TeamRequest>
    {

        public void Configure(EntityTypeBuilder<TeamRequest> builder)
        {
            builder
                .HasKey(pk => new { pk.RequestFromId, pk.TeamId });



            builder
                .HasOne(e => e.Team)
                .WithMany(e => e.TeamRequests)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.RequestFrom)
                .WithMany(e => e.TeamRequests)
                .HasForeignKey(e => e.RequestFromId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
