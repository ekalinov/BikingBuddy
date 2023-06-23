using System.Reflection;
using BikingBuddy.Common;
using BikingBuddy.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace BikingBuddy.Data
{
    public class BikingBuddyDbContext : IdentityDbContext<AppUser,IdentityRole<Guid> ,Guid>
    {

        public BikingBuddyDbContext(DbContextOptions<BikingBuddyDbContext> options)
            : base(options)
        {

        }
        
        public DbSet<AppUser> AppUser { get; set; } = null!;

        public DbSet<Activity> Activities { get; set; } = null!;

        public DbSet<ActivityType> RideTypes { get; set; } = null!;

        public DbSet<Event> Event { get; set; } = null!;

        public DbSet<Bike> Bike { get; set; } = null!;

        public DbSet<BikeType> BikeType { get; set; } = null!;

        public DbSet<Comment> Comment { get; set; } = null!;

        public DbSet<Country> Country { get; set; } = null!;

        public DbSet<Municipality> Municipality { get; set; } = null!;

        public DbSet<Town> Town { get; set; } = null!;

        public DbSet<Activity> RideType { get; set; } = null!;

        public DbSet<EventParticipants> EventsParticipants { get; set; } = null!;

        public DbSet<UserActivity> UsersActivities { get; set; } = null!; 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
         Assembly configAssembly = Assembly.GetAssembly(typeof(BikingBuddyDbContext))  
                                                ?? Assembly.GetExecutingAssembly();

         modelBuilder.ApplyConfigurationsFromAssembly(configAssembly);
  

            base.OnModelCreating(modelBuilder);

        }







    }
}