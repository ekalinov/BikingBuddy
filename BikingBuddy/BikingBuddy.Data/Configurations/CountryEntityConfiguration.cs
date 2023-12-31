﻿using BikingBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using BikingBuddy.Data.Configurations.Seeders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikingBuddy.Data.Configurations
{
    public class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(CountrySeeder.GetCountries());
        }
    }
}
