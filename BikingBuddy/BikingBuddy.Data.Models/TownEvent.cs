using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Data.Models
{
    public class TownEvent
    {
        [Required]
        public int  TownId { get; set; } 

        [ForeignKey(nameof(TownId))]
        public Town Town { get; set; } = null!;

        [Required]
        public string EventId { get; set; } = null!;

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;



    }
}
