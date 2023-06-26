namespace BikingBuddy.Data.Configurations.Seeders
{ 
    using Models;
    
    internal class ActivityTypeSeeder
    {
        internal static ActivityType[] GetActivityTypes()
        {

            ICollection<ActivityType> rideTypes = new HashSet<ActivityType>()
            {
                new ActivityType(){Id = 1, Name = "Mountain Biking"},
                new ActivityType(){Id = 2, Name = "Road Cycling"},
                new ActivityType(){Id = 3, Name = "Gravel Biking"},
                new ActivityType(){Id = 4, Name = "Urban Cycling"},
                new ActivityType(){Id = 5, Name = "Downhill Track"},
                new ActivityType(){Id = 6, Name = "Trail"},
                new ActivityType(){Id = 7, Name = "Bikepacking"},
                new ActivityType(){Id = 8, Name = "Race"},
                new ActivityType(){Id = 9,Name="Other"}
            };

            return rideTypes.ToArray();
        }

    }
}
