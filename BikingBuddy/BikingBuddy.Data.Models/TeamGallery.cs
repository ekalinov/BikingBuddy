using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static BikingBuddy.Common.EntityValidationsConstants.TeamGallery;

namespace BikingBuddy.Data.Models;

public class TeamGalleryPhoto
{
    public TeamGalleryPhoto()
    {
         Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }

    [Required]
    [MaxLength(TeamGalleryPhotoNameMaxLength)]
    public string Name { get; set; } = null!;

    [Url]
    [Required]
    public string Url { get; set; }= null!;

    public Team Team { get; set; }= null!;

    [ForeignKey(nameof(Team))]
    [Required]
    public Guid TeamId { get; set; }  
}