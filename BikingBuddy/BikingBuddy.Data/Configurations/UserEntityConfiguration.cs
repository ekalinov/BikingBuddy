namespace BikingBuddy.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    internal class UserEntityConfiguration : IEntityTypeConfiguration<AppUser>

    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
                .HasOne(e => e.Town)
                .WithMany(e => e.TownUsers)
                .HasForeignKey(e => e.TownId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Country)
                .WithMany(e => e.CountryUsers)
                .HasForeignKey(e => e.CountryId)
                .OnDelete(DeleteBehavior.Restrict);



            builder
                .Property(e => e.TownId)
                .IsRequired(false);

            builder
                .Property(e => e.CountryId)
                .IsRequired(false);

        }
    }
}
