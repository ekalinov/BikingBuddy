namespace BikingBuddy.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static BikingBuddy.Common.EntityValidationsConstants.Municipality;


    public class Municipality
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(MunicipalityNameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
