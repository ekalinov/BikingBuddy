using BikingBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
