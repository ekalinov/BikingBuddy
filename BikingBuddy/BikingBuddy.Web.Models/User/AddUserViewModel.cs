using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using static BikingBuddy.Common.EntityValidationsConstants.User;

namespace BikingBuddy.Web.Models.User
{
    public class AddUserViewModel
    {
        public AddUserViewModel()
        {
            CountriesCollection = new HashSet<CountryViewModel>();
        }


        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string Name { get; set; } = null!;

        [Url] public string? ProfileImageUrl { get; set; }

        public string? CountryId { get; set; }
        public string? Town { get; set; }


        [StringLength(HelmetNameMaxLength)] public string? Helmet { get; set; }


        [StringLength(ShoesNameMaxLength)] public string? Shoes { get; set; }

        [Display(Name = "Choose the profile photo")]
        public IFormFile? ProfileImage { get; set; }

        [NotMapped]
        [FileExtensions(ErrorMessage = "Supported file extensions are .jpg, .jpeg and .png")]
        public string? FileName => ProfileImage?.FileName;

        public virtual ICollection<CountryViewModel> CountriesCollection { get; set; }
    }
}