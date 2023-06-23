namespace BikingBuddy.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.EntityValidationsConstants.User;


    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            this.EventsParticipants = new HashSet<EventParticipants>();
            this.UserActivities = new HashSet<UserActivity>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [ForeignKey(nameof(BikeId))]
        public Bike? Bike { get; set; } = null!;

        public string? BikeId { get; set; } = null!;

        public string? Helmet { get; set; } = null!;

        public string? Shoes { get; set; } = null!;

        public string TeamId { get; set; } = null!;

        [ForeignKey(nameof(TeamId))]
        public Team? Team { get; set; } = null!;

        [Url]
        public string? ProfileImageUrl { get; set; }



        public ICollection<UserActivity> UserActivities { get; set; }

        public ICollection<EventParticipants> EventsParticipants { get; set; } 

    }
}
