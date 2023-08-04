using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BikingBuddy.Common.EntityValidationsConstants.EventGallery;

namespace BikingBuddy.Data.Models;

public class EventGalleryPhoto
{
    
    public EventGalleryPhoto()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }

    [Required]
    [MaxLength(EventGalleryPhotoNameMaxLength)]
    public string Name { get; set; } = null!;
 

    [Url]
    [Required]
    public string Url { get; set; }= null!;

    public Event Event { get; set; }= null!;

    [ForeignKey(nameof(Event))]
    [Required]
    public Guid EventId { get; set; }  
}