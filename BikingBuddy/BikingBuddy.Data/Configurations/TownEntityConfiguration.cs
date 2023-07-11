namespace BikingBuddy.Data.Configurations
{
    using Models;
    using Seeders;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    internal class TownEntityConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasData(TownSeeder.GetTowns());
        }
    }
}
