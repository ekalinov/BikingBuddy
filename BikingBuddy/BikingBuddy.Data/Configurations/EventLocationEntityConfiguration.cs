using BikingBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikingBuddy.Data.Configurations;

public class EventLocationEntityConfiguration : IEntityTypeConfiguration<EventLocation>
{
    public void Configure(EntityTypeBuilder<EventLocation> builder)
    {
        builder
            .HasKey(el => el.EventId);
         
        builder
            .Property(el => el.Latitude)
            .HasDefaultValue(42.69);
        
        builder
            .Property(el => el.Longitude)
            .HasDefaultValue(23.32);
    }
}