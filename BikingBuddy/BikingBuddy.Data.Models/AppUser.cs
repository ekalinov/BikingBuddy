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
            this.Id = Guid.NewGuid();
            this.EventsParticipants = new HashSet<EventParticipants>();
            this.TeamRequests = new HashSet<TeamRequest>();
            this.UserBikes = new HashSet<Bike>();
        }



        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Name { get; set; } = null!;

        [Url]
        public string? ProfileImageUrl { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; } = null!;

        public string? CountryId { get; set; } 




        [ForeignKey(nameof(TownId))]
        public virtual Town Town { get; set; } = null!;

        public int? TownId { get; set; }


        public string? Helmet { get; set; } = null!;

        public string? Shoes { get; set; } = null!;




        public Guid? TeamId { get; set; } = null!;
        
        [ForeignKey(nameof(TeamId))]
        public virtual Team? Team { get; set; } = null!;
        

        public virtual ICollection<EventParticipants> EventsParticipants { get; set; }

        
        public virtual ICollection<TeamRequest> TeamRequests { get; set; }


        public virtual ICollection<Bike> UserBikes { get; set; }




    }
}
