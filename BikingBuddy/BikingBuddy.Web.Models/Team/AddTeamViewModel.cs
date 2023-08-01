using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
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

        [Display(Name = "Choose your Team photo")]    
        public IFormFile? TeamImage { get; set; }

        [NotMapped]
        [FileExtensions(ErrorMessage = "Supported file extensions are .jpg, .jpeg and .png")]
        public string? FileName => TeamImage?.FileName;


        public ICollection<CountryViewModel> CountriesCollection { get; set; }

        [Required]
        public string CountryId { get; set; } = null!;

        [Required]
        public string TownName { get; set; } = null!;
        

    }
}
