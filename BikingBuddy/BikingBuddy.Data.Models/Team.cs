namespace BikingBuddy.Data.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static BikingBuddy.Common.EntityValidationsConstants.Team;
    
    public class Team
    {
        public Team()
        {
            this.Id = Guid.NewGuid();

            this.TeamMembers = new HashSet<AppUser>();
            this.TeamRequests = new HashSet<TeamRequest>();
        }


        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TeamNameMaxLength)] 
        
        public string Name { get; set; } = null!;

        [MaxLength(TeamDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public DateTime? EstablishedOn { get; set; }

        [Url]
        public string? TeamImageUrl { get; set; }


        [ForeignKey(nameof(CountryId))]
        public virtual Country Country { get; set; } = null!;

        public string CountryId { get; set; } = null!;


        [ForeignKey(nameof(TownId))]
        public virtual Town Town { get; set; } = null!;

        [Required]
        public int TownId { get; set; }


        public virtual ICollection<AppUser> TeamMembers { get; set; }

        public virtual ICollection<TeamRequest> TeamRequests { get; set; }

        public virtual AppUser TeamManager { get; set; } = null!;

        [ForeignKey(nameof(TeamManager))]
        public Guid TeamManagerId { get; set; }


    }
}
