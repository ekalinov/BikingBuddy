using System.ComponentModel.DataAnnotations.Schema;

namespace BikingBuddy.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static BikingBuddy.Common.EntityValidationsConstants.Ride;


    public class Activity
    {
        public Activity()
        {
            this.UserActivities = new HashSet<UserActivity>();
        }

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

        [Required]
        public int RideTypeId { get; set; }

        [ForeignKey(nameof(RideTypeId))]
        public ActivityType ActivityType { get; set; } = null!;

        public ICollection<UserActivity> UserActivities { get; set; }

    }
}
