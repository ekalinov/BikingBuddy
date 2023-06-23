using BikingBuddy.Common;
using BikingBuddy.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace BikingBuddy.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<AppUser> AppUser { get; set; } = null!;

        public DbSet<Event> Event { get; set; } = null!;

        public DbSet<Bike> Bike { get; set; } = null!;

        public DbSet<BikeType> BikeType { get; set; } = null!;

        public DbSet<Comment> Comment { get; set; } = null!;

        public DbSet<Country> Country { get; set; } = null!;

        public DbSet<Municipality> Municipality { get; set; } = null!;

        public DbSet<Town> Town { get; set; } = null!;

        public DbSet<RideType> RideType { get; set; } = null!;

        public DbSet<EventParticipants> EventsParticipants { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EventParticipants>()
                .HasKey(pk => new { pk.EventId, pk.ParticipantId });

            modelBuilder.Entity<TownEvent>()
                .HasKey(pk => new { pk.EventId, pk.TownId });

            modelBuilder.Entity<EventParticipants>()
                .HasOne(e => e.Event)
                .WithMany(e => e.EventsParticipants)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EventParticipants>()
                .HasOne(e => e.Participant)
                .WithMany(e => e.EventsParticipants)
                .HasForeignKey(e => e.ParticipantId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(e => e.Event)
                .WithMany(e => e.EventComments)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Town>()
                .HasMany(e => e.TownEvents);

            modelBuilder.Entity<TownEvent>()
                .HasOne(e => e.Town)
                .WithMany(e => e.TownEvents)
                .HasForeignKey(e => e.TownId)
                .OnDelete(DeleteBehavior.Restrict);

            


            base.OnModelCreating(modelBuilder);

        }







    }
}