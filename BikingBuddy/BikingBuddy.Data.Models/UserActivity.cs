using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Data.Models
{
    public class UserActivity
    {
        [Required]
        public Guid AppUserId { get; set; }
        
        [ForeignKey(nameof(AppUserId))]
        public AppUser AppUser { get; set; } = null!;

        [Required]
        public int ActivityId { get; set; } 

        [ForeignKey(nameof(ActivityId))]
        public Activity Activity { get; set; } = null!;
    }
}
