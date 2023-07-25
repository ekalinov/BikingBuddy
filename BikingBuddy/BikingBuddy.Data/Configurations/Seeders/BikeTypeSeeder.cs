using BikingBuddy.Data.Models;

namespace BikingBuddy.Data.Configurations.Seeders
{
    internal class BikeTypeSeeder
    {
        public static BikeType[] GetBikeTypes()
        {
            ICollection<BikeType> bikeTypes = new HashSet<BikeType>()
            {
                new BikeType(){Id = 1, Name = "Mountain Bike"},
                new BikeType(){Id = 2, Name = "Road Bike"},
                new BikeType(){Id = 3, Name = "Gravel Bike"},
                new BikeType(){Id = 4, Name = "Urban Bike"},
                new BikeType(){Id = 5, Name = "Downhill Bike"},
                new BikeType(){Id = 6, Name = "Trail Bike"},
                new BikeType(){Id = 7, Name = "Fat Bike"},
                new BikeType(){Id = 8, Name = "BMX"},
                new BikeType(){Id = 9,Name = "Other"}
            };

            return bikeTypes.ToArray();


        }
    }
}
