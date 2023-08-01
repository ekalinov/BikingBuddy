using System;

namespace BikingBuddy.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EventParticipants
    {
        [Required]
        public Guid ParticipantId { get; set; } 


        [ForeignKey(nameof(ParticipantId))]
        public virtual AppUser Participant { get; set; } = null!;

        [Required]
        public Guid EventId { get; set; } 

        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; } = null!;

        public bool IsCompleted { get; set; } = false;

    }
}
