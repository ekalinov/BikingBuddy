namespace BikingBuddy.Data.Configurations.Seeders
{
    using BikingBuddy.Data.Models;

    internal class MunicipalitySeeder
    {
        public static Municipality[] GetMunicipalities()
        {
            ICollection<Municipality> municipalities = new HashSet<Municipality>()
            {
                new Municipality(){Id = 1, Name = "Sofia-city"},
                new Municipality(){Id = 2, Name = "Plovdiv"},
                new Municipality(){Id = 3, Name = "Madan"},
                new Municipality(){Id = 4, Name = "Smolyan"},

            };

            return municipalities.ToArray();


        }
    }
}
