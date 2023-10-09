using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static BikingBuddy.Common.EntityValidationsConstants.EventTrack;

namespace BikingBuddy.Data.Models;

public class EventTrack
{
    [Key, ForeignKey("Event")]
    public Guid EventId { get; set; }

    [Required]
    [MaxLength(FileNameMaxLenght)]
    public string FileName { get; set; } = null!;

    [Required]
    [MaxLength(FileContentMaxLenght)]
    public string GPXContent { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}