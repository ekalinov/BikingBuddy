using BikingBuddy.Common.Enums;

namespace BikingBuddy.Web.Models.Bike
{
    public class BikeDetailsViewModel
    {
        public string Id { get; set; } = null!;

        public string? FrameBrand { get; set; } 

        public string? FrameSize { get; set; }

        public double WheelSize { get; set; }

        public string? WheelBrand { get; set; }

        public string? Drivetrain { get; set; }

        public string? Fork { get; set; }

        public BikeTypes BikeType { get; set; }
    }
}