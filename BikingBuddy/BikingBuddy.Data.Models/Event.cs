namespace BikingBuddy.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static BikingBuddy.Common.EntityValidationsConstants.Event;

    public class Event
    {

        public Event()
        {
            this.Id = Guid.NewGuid().ToString();

            this.EventComments = new HashSet<Comment>();
            this.EventsParticipants = new HashSet<EventParticipants>();
        }


        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public int RideTypeId { get; set; }

        [ForeignKey(nameof(RideTypeId))]
        public RideType RideType { get; set; } = null!;


        [Required]
        public string OrganizerId { get; set; } = null!;

        [ForeignKey(nameof(OrganizerId))]
        public AppUser Organizer { get; set; } = null!;
        


        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; } = null!;

        public string CountryId { get; set; } = null!;


        [ForeignKey(nameof(MunicipalityId))]
        public Municipality Municipality { get; set; } = null!;

        public int? MunicipalityId { get; set; }


        [ForeignKey(nameof(TownId))]
        public Town Town { get; set; } = null!;

        [Required]
        public int TownId { get; set; }
        

        public ICollection<Comment> EventComments { get; set; }

        public ICollection<EventParticipants> EventsParticipants { get; set; }

    }


}
