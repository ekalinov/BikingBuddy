using System.ComponentModel.DataAnnotations;
using static BikingBuddy.Common.EntityValidationsConstants.Team;

namespace BikingBuddy.Web.Models.Team
{
    public class AddTeamViewModel
    {
        public AddTeamViewModel()
        {
            CountriesCollection = new HashSet<CountryViewModel>();
        }


        [Required]
        [MaxLength(TeamNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(TeamDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        
        public DateTime? EstablishedOn { get; set; }

        [Url]
        public string? TeamImageUrl { get; set; }


        public ICollection<CountryViewModel> CountriesCollection { get; set; }

        [Required]
        public string CountryId { get; set; } = null!;

        [Required]
        public string TownName { get; set; } = null!;
        

    }
}
