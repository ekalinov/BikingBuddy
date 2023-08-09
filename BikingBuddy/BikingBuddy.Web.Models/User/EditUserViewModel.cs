using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using static BikingBuddy.Common.EntityValidationsConstants.User;
namespace BikingBuddy.Web.Models.User
{
    public class EditUserViewModel: AddUserViewModel
    {
        public EditUserViewModel()
        {
            CountriesCollection = new HashSet<CountryViewModel>();
        }
        
        
        
        public string Id { get; set; } = null!;
        
        

    }
}