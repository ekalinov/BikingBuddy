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
        public string Descriptions { get; set; } = null!;

        public DateTime? EstablishedOn { get; set; }

        [Url]
        public string? TeamImageUrl { get; set; }


        public ICollection<AppUser> TeamMembers { get; set; }



        public AppUser TeamManager { get; set; } = null!;

        [ForeignKey(nameof(TeamManager))]
        public Guid TeamManagerId { get; set; }


    }
}
