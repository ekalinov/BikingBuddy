using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BikingBuddy.Web.Models;

public class AddGalleryPhotoModel
{
    public AddGalleryPhotoModel()
    { 
        GalleryPhotosModels = new HashSet<GalleryPhotoModel>();  
    }

    public string EventId { get; set; } = null!;

    public string EventName { get; set; } = null!;
    public IFormFileCollection? GalleryPhotos { get; set; }  
     
    public ICollection<GalleryPhotoModel>? GalleryPhotosModels { get; set; }
}