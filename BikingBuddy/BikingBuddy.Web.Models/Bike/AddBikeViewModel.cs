using BikingBuddy.Web.Models.BikeType;
using System.ComponentModel.DataAnnotations;
using static BikingBuddy.Common.EntityValidationsConstants.Bike;

namespace BikingBuddy.Web.Models.Bike
{
    public class AddBikeViewModel
    {
        public AddBikeViewModel()
        {
            BikeTypes = new HashSet<BikeTypeViewModel>();
        }
       
        [StringLength(FrameBrandMaxLength, MinimumLength = FrameBrandMinLength)]
        public string? FrameBrand { get; set; }

        [StringLength(FrameSizeMaxLength, MinimumLength = FrameSizeMinLength)]
        public string? FrameSize { get; set; }

        [Required]
        [Range(0,30)]
        public double WheelSize { get; set; }

        [StringLength(WheelBrandMaxLength, MinimumLength = WheelBrandMinLength)]
        public string? WheelBrand { get; set; }

        [StringLength(DrivetrainMaxLength, MinimumLength = DrivetrainMinLength)]
        public string? Drivetrain { get; set; }

        [StringLength(ForkMaxLength, MinimumLength = ForkMinLength)]
        public string? Fork { get; set; }


        public virtual ICollection<BikeTypeViewModel> BikeTypes { get; set; }

        [Required]
        public int BikeTypesId { get; set; } 

    }
}
