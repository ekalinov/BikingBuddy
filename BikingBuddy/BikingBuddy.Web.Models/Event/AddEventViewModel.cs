using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BikingBuddy.Common;
using BikingBuddy.Web.Models.Activity;  

using static BikingBuddy.Common.EntityValidationsConstants.Event;
using static BikingBuddy.Common.EntityValidationsConstants.Municipality;
using static BikingBuddy.Common.EntityValidationsConstants.Town;

namespace BikingBuddy.Web.Models.Event
{
    public class AddEventViewModel
    {
        public AddEventViewModel()
        {
            ActivityTypes = new HashSet<ActivityTypeViewModel>();
            CountriesCollection = new HashSet<CountryViewModel>();
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

        [Url]
        public string? EventImageUrl { get; set; }

        public ICollection<ActivityTypeViewModel> ActivityTypes { get; set; }

        public int ActivityTypeId { get; set; }


        [Required]
        [Range(0, 1000)]
        public double Distance { get; set; }  

        [Required]
        [Range(0,1000)]
        public double Ascent { get; set; } 


        public ICollection<CountryViewModel> CountriesCollection { get; set; }

        public string CountryId { get; set; } = null!;

        [StringLength(MunicipalityNameMaxLength,
            ErrorMessage = "Municipality must be between {2} and {1}", MinimumLength = MunicipalityNameMinLength)]

        public string? Municipality { get; set; }


        [Required]
        [StringLength(TownNameMaxLength,
            ErrorMessage = "Municipality must be between {2} and {1}", MinimumLength = TownNameMinLength)]
        public string TownName { get; set; } = null!;
    }


}
