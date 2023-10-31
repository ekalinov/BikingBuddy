using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BikingBuddy.Web.Models.Event;

public class UploadGPXFileViewModel
{
    public string EventId { get; set; } = null!;
     
    [Display(Name = "Upload one or more tracks for your event")]
    public IFormFile? EventTrackFile { get; set; }
    
}