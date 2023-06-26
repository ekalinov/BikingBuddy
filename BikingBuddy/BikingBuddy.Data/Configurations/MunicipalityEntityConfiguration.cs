using BikingBuddy.Data.Configurations.Seeders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikingBuddy.Data.Models;

namespace BikingBuddy.Data.Configurations
{
    internal class MunicipalityEntityConfiguration : IEntityTypeConfiguration<Municipality>
    {
        public void Configure(EntityTypeBuilder<Municipality> builder)
        {
            builder.HasData(MunicipalitySeeder.GetMunicipalities());
        }
    }
}
