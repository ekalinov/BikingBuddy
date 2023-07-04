using BikingBuddy.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static BikingBuddy.Common.EntityValidationsConstants.Team;

namespace BikingBuddy.Web.Models.Team
{
    public class AddTeamViewModel
    {
        public AddTeamViewModel()
        {
            this.CountriesCollection = new HashSet<CountryViewModel>();
        }

        [Required]
        [MaxLength(TeamNameMaxLength)]

        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(TeamDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime EstablishedOn { get; set; }

        [Url]
        public string? TeamImageUrl { get; set; }


        public ICollection<CountryViewModel> CountriesCollection { get; set; }

        [Required]
        public string CountryId { get; set; } = null!;

        [Required]
        public string TownName { get; set; } = null!;
        

    }
}
