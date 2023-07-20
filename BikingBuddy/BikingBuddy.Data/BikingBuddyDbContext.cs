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

        public BikingBuddyDbContext()
        {
            
        }
        public BikingBuddyDbContext(DbContextOptions<BikingBuddyDbContext> options)
            : base(options)
        {

        }
        
        public DbSet<AppUser> AppUsers { get; set; } = null!;
        
        public DbSet<ActivityType> ActivityTypes { get; set; } = null!;

        public DbSet<Event> Events { get; set; } = null!;

        public DbSet<Team> Teams { get; set; } = null!;

        public DbSet<TeamRequest> TeamsRequests { get; set; } = null!;

        public DbSet<Bike> Bikes { get; set; } = null!;
        
        public DbSet<BikeType> BikeTypes { get; set; } = null!;

        public DbSet<Comment> Comments { get; set; } = null!;

        public DbSet<Country> Countries { get; set; } = null!;

        public DbSet<Town> Towns { get; set; } = null!;
        
        public DbSet<EventParticipants> EventsParticipants { get; set; } = null!;
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
         Assembly configAssembly = Assembly.GetAssembly(typeof(BikingBuddyDbContext))  
                                                ?? Assembly.GetExecutingAssembly();

         modelBuilder.ApplyConfigurationsFromAssembly(configAssembly);
  

            base.OnModelCreating(modelBuilder);

        }

       
        
    }
}