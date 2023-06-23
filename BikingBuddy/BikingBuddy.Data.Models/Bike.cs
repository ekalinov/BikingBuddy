namespace BikingBuddy.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static BikingBuddy.Common.EntityValidationsConstants.Bike;

    public class Bike
    {
        public Bike()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [MaxLength(FrameBrandMaxLength)]
        public string? FrameBrand { get; set; }

        [MaxLength(FrameSizeMaxLength)]
        public string? FrameSize { get; set; }

        [Required]
        public double WheelSize { get; set; }

        [MaxLength(FrameBrandMaxLength)]
        public string? WheelBrand { get; set; }

        [MaxLength(DrivetrainMaxLength)]
        public string? Drivetrain { get; set; }

        [MaxLength(ForkMaxLength)]
        public string? Fork { get; set; }

        [Required]
        [ForeignKey(nameof(BikeType))]
        public int BikeTypeId { get; set; }

        public BikeType BikeType { get; set; } = null!;
    }


}
