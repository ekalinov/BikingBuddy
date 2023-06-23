namespace BikingBuddy.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static BikingBuddy.Common.EntityValidationsConstants.RideType;


    public class RideType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(NameMaxLength)]
        public double TotalDistance { get; set; }


        [Required]
        public double TotalAscent { get; set; } 
    }
}
