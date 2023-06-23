namespace BikingBuddy.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Seeders;
    using Models;

    internal class ActivityTypeEntityConfiguration : IEntityTypeConfiguration<ActivityType>
    {
        public void Configure(EntityTypeBuilder<ActivityType> builder)
        {
            builder.HasData(ActivityTypeSeeder.GetActivityTypes());
        }
        
    }


}
