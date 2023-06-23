namespace BikingBuddy.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static BikingBuddy.Common.EntityValidationsConstants.Region;


    public class Municipality
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
