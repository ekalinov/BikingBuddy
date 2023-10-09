using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BikingBuddy.Data.Models;

public class EventLocation
{ 
    [Key, ForeignKey("Event")]
    public Guid EventId { get; set; }
    
    [Required]
    public double Longitude { get; set; } 
    
    [Required]
    public double Latitude { get; set; }

    public virtual Event Event { get; set; } = null!;
}