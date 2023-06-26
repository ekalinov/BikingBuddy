using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikingBuddy.Data.Models;

namespace BikingBuddy.Data.Configurations.Seeders
{
    internal class TownSeeder
    {

        public static Town[] GetTowns()
        {
            ICollection<Town> towns = new HashSet<Town>()
            {
                new Town(){Id = 1, Name = "Sofia"},
                new Town(){Id = 2, Name = "Plovdiv"},
                new Town(){Id = 3, Name = "Madan"},
                new Town(){Id = 4, Name = "Smolyan"},
                new Town(){Id = 5, Name = "Bourgas"},
                new Town(){Id = 6, Name = "Varna"},
                new Town(){Id = 7, Name = "Yambol"},
                new Town(){Id = 8, Name = "Devin"}
            };

            return towns.ToArray();


        }
    }
}
