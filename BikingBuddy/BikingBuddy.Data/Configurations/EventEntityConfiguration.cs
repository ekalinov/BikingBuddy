namespace BikingBuddy.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class EventEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder
                .HasOne(e => e.Town)
                .WithMany(e => e.TownEvents)
                .HasForeignKey(e => e.TownId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Country)
                .WithMany(e => e.CountryEvents)
                .HasForeignKey(e => e.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
