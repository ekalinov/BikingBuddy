namespace BikingBuddy.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static BikingBuddy.Common.EntityValidationsConstants.Event;

    public class Event
    {

        public Event()
        {
            Id = Guid.NewGuid();

            EventComments = new HashSet<Comment>();
            EventsParticipants = new HashSet<EventParticipants>();
        }


        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public DateTime CreatedOn { get; set; }


        
        [Required]
        public double Distance { get; set; }

        [Required]
        public double Ascent { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Url]
        public string? EventImageUrl { get; set; }

        [Required]
        public int ActivityTypeId { get; set; }

        [ForeignKey(nameof(ActivityTypeId))]
        public virtual ActivityType ActivityType { get; set; } = null!;


        [Required]
        public Guid OrganizerId { get; set; } 

        [ForeignKey(nameof(OrganizerId))]
        public virtual AppUser Organizer { get; set; } = null!;
        

        

        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; } = null!;

        public string CountryId { get; set; } = null!;



        [ForeignKey(nameof(TownId))]
        public virtual Town Town { get; set; } = null!;

        [Required]
        public int TownId { get; set; }
        
        
        public bool IsDeleted { get; set; } = false;
        

        public virtual ICollection<Comment> EventComments { get; set; }

        public virtual ICollection<EventParticipants> EventsParticipants { get; set; }

    }


}
