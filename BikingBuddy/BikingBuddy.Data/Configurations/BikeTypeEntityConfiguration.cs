using BikingBuddy.Data.Configurations.Seeders;

namespace BikingBuddy.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    internal class BikeTypeEntityConfiguration : IEntityTypeConfiguration<BikeType>
    {
        public void Configure(EntityTypeBuilder<BikeType> builder)
        {
            builder.HasData(BikeTypeSeeder.GetBikeTypes());
        }
    }
}
