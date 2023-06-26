namespace BikingBuddy.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static BikingBuddy.Common.EntityValidationsConstants.ActivityType;

    public class ActivityType
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ActivityTypeNameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
