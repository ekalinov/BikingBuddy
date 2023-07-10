using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using BikingBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikingBuddy.Data.Configurations
{
    internal class EventParticipantEntityConfiguration :IEntityTypeConfiguration<EventParticipants>
    {
        public void Configure(EntityTypeBuilder<EventParticipants> builder)
        {
            builder
                .HasKey(pk => new { pk.EventId, pk.ParticipantId });



            builder
                .HasOne(e => e.Event)
                .WithMany(e => e.EventsParticipants)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Participant)
                .WithMany(e => e.EventsParticipants)
                .HasForeignKey(e => e.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
