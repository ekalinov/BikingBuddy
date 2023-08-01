using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikingBuddy.Data.Models
{
    public class TeamRequest
    {
        [Required]
        public Guid RequestFromId { get; set; }


        [ForeignKey(nameof(RequestFromId))]
        public virtual AppUser RequestFrom { get; set; } = null!;

        [Required]
        public Guid TeamId { get; set; }

        [ForeignKey(nameof(TeamId))]
        public virtual Team Team { get; set; } = null!;

        public bool IsAccepted { get; set; } = false;


    }
}
