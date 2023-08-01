using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikingBuddy.Web.Models.User
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            CountriesCollection = new HashSet<CountryViewModel>();
        }

        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;
        public ICollection<CountryViewModel> CountriesCollection { get; set; }

        public string CountryId { get; set; } = null!;

        public string Town { get; set; } = null!;

        public string Country { get; set; } = null!;


        public string? Helmet { get; set; } = null!;

        public string? Shoes { get; set; } = null!;

        public string? Team { get; set; } = null!;


        [Url]
        public string? ProfileImageUrl { get; set; }
    }
}
