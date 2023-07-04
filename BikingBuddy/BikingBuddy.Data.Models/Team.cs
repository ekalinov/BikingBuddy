namespace BikingBuddy.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static BikingBuddy.Common.EntityValidationsConstants.Team;
    
    public class Team
    {
        public Team()
        {
            this.Id = Guid.NewGuid();

            this.TeamMembers = new HashSet<AppUser>();
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
        public Country Country { get; set; } = null!;

        public string CountryId { get; set; } = null!;


        [ForeignKey(nameof(TownId))]
        public Town Town { get; set; } = null!;

        [Required]
        public int TownId { get; set; }


        public ICollection<AppUser> TeamMembers { get; set; }



        public AppUser TeamManager { get; set; } = null!;

        [ForeignKey(nameof(TeamManager))]
        public Guid TeamManagerId { get; set; }


    }
}
