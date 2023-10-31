using BikingBuddy.Common.Enums;
using BikingBuddy.Data.Models;

namespace BikingBuddy.Web.Models.Event
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Activity;
    using Microsoft.AspNetCore.Http;
    using static BikingBuddy.Common.EntityValidationsConstants.Event; 

    using static Common.EntityValidationsConstants.Town;
    public class AddEventViewModel
    {
        public AddEventViewModel()
        {
            ActivityTypes = new HashSet<ActivityTypeViewModel>();
            CountriesCollection = new HashSet<CountryViewModel>();
            GalleryPhotosModels = new List<GalleryPhotoModel>(); 
        }


        [Required]
        [StringLength(TitleMaxLength,
            ErrorMessage = "Title must be between {2} and {1}", MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        public string Date { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength,
            ErrorMessage = "Description must be between {2} and {1}", MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Url] public string? EventImageUrl { get; set; }

        public ICollection<ActivityTypeViewModel> ActivityTypes { get; set; }

        public int ActivityTypeId { get; set; }
 
        [Required] [Range(0, 2000)] public double Distance { get; set; }

        [Required] [Range(0, 5000)] public double Ascent { get; set; }

        [Display(Name = "Choose the cover photo of your event")]
        public IFormFile? EventImage { get; set; }

        [NotMapped]
        [FileExtensions(ErrorMessage = "Supported file extensions are .jpg, .jpeg and .png")]
        public string? FileName => EventImage?.FileName;

        [Display(Name = "Choose your Event Gallery photos")]
        public ICollection<GalleryPhotoModel>? GalleryPhotosModels { get; set; }
  
        public IFormFileCollection? GalleryPhotos { get; set; }
 
        public ICollection<CountryViewModel> CountriesCollection { get; set; }

        public string CountryId { get; set; } = null!;
 
        [Required]
        [StringLength(TownNameMaxLength,
            ErrorMessage = "Town must be between {2} and {1}", MinimumLength = TownNameMinLength)]
        public string TownName { get; set; } = null!;
        
        [Required]
        public double Latitude { get; set; }
        
        [Required]
        public double Longitude { get; set; }
        
       
        [Range(0, 5000)] public double Price { get; set; }
        
        public Currencies Currency { get; set; }
        
        [Display(Name = "Upload one or more tracks for your event")]
        public IFormFile? EventTrackFile { get; set; }
        
        public EventTrack? EventTrack { get; set; }

    }
    
} 