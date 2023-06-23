namespace BikingBuddy.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EventParticipants
    {
        [Required]
        public string ParticipantId { get; set; } = null!;

        [ForeignKey(nameof(ParticipantId))]
        public AppUser Participant { get; set; } = null!;

        [Required]
        public string EventId { get; set; } = null!;

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;
    }
}
