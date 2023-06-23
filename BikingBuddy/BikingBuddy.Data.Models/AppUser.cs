namespace BikingBuddy.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.EntityValidationsConstants.User;


    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            this.EventsParticipants = new HashSet<EventParticipants>();
            this.RideTypes = new HashSet<RideType>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [ForeignKey(nameof(BikeId))]
        public Bike? Bike { get; set; } = null!;

        public string? BikeId { get; set; } = null!;

        public string? Helmet { get; set; } = null!;

        public string? Shoes { get; set; } = null!;


        public ICollection<RideType> RideTypes { get; set; } 

        public ICollection<EventParticipants> EventsParticipants { get; set; } 

    }
}
