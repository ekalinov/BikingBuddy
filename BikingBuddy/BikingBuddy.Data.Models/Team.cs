﻿namespace BikingBuddy.Data.Models
{

    using System.ComponentModel.DataAnnotations;

    using static BikingBuddy.Common.EntityValidationsConstants.Team;
    
    public class Team
    {
        public Team()
        {
            this.Id = Guid.NewGuid().ToString();

            this.TeamMembers = new HashSet<AppUser>();
        }


        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(TeamNameMaxLength)] 
        
        public string Name { get; set; } = null!;
        
        public DateTime? EstablishedOn { get; set; }

        [Url]
        public string? TeamImageUrl { get; set; }

        public ICollection<AppUser> TeamMembers { get; set; }


    }
}
