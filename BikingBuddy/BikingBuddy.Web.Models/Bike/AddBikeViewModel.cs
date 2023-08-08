using BikingBuddy.Web.Models.BikeType;
using System.ComponentModel.DataAnnotations;
using BikingBuddy.Common.Enums;
using BikingBuddy.Data.Models; 
using static BikingBuddy.Common.EntityValidationsConstants.Bike;

namespace BikingBuddy.Web.Models.Bike
{
    public class AddBikeViewModel
    {
       
        

        [StringLength(FrameBrandMaxLength, MinimumLength = FrameBrandMinLength)]
        public string? FrameBrand { get; set; }

        [StringLength(FrameSizeMaxLength, MinimumLength = FrameSizeMinLength)]
        public string? FrameSize { get; set; }

        [Required] [Range(0, 30)] public double WheelSize { get; set; }

        [StringLength(WheelBrandMaxLength, MinimumLength = WheelBrandMinLength)]
        public string? WheelBrand { get; set; }

        [StringLength(DrivetrainMaxLength, MinimumLength = DrivetrainMinLength)]
        public string? Drivetrain { get; set; }

        [StringLength(ForkMaxLength, MinimumLength = ForkMinLength)]
        public string? Fork { get; set; }


        public virtual BikeTypes BikeType { get; set; }
  
        public  string OwnerId { get; set; }
    }
}