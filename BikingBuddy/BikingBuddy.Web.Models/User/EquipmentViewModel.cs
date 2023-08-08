using System.ComponentModel.DataAnnotations;

namespace BikingBuddy.Web.Models.User
{
    using static BikingBuddy.Common.EntityValidationsConstants.User;

    public class EquipmentViewModel
    {
        //UserId 
        public string  Id { get; set; } = null!;
        
        [StringLength(HelmetNameMaxLength)] public string? Helmet { get; set; }

        [StringLength(ShoesNameMaxLength)] public string? Shoes { get; set; }
    }
}