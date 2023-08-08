using BikingBuddy.Common.Enums;

namespace BikingBuddy.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static BikingBuddy.Common.EntityValidationsConstants.Bike;

    public class Bike
    {
        public Bike()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [MaxLength(FrameBrandMaxLength)]
        public string? FrameBrand { get; set; }

        [MaxLength(FrameSizeMaxLength)]
        public string? FrameSize { get; set; }

        [Required]
        public double WheelSize { get; set; }

        [MaxLength(WheelBrandMaxLength)]
        public string? WheelBrand { get; set; }

        [MaxLength(DrivetrainMaxLength)]
        public string? Drivetrain { get; set; }

        [MaxLength(ForkMaxLength)]
        public string? Fork { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Required]

        public BikeTypes BikeType { get; set; } 


        [Required]
        [ForeignKey(nameof(AppUser))]
        public Guid AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;

    }


}
