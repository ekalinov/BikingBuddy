using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string EventId { get; set; } = null!;

        [ForeignKey(nameof(EventId))] 
        public Event Event { get; set; } = null!;
        

        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; } = null!;

        [Required]
        public string CommentBody { get; set; } = null!;
    }
}
