using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Common
{
    public class EntityValidationsConstants
    {
        public class User
        {
            public const int NameMaxLength = 30;
            public const int NameMinLength = 5;
        }

        public class Bike
        {
            public const int FrameBrandMaxLength = 20;
            public const int FrameBrandMinLength = 2;

            public const int FrameSizeMaxLength = 2;

            public const int DrivetrainMaxLength = 20;
            public const int DrivetrainMinLength = 2;


            public const int ForkMaxLength = 20;
            public const int ForkMinLength = 2;
        }

        public class BikeType
        {
            public const int NameMaxLength = 20;
            public const int NameMinLength = 2;
        }

        public class RideType
        {
            public const int NameMaxLength = 20;
            public const int NameMinLength = 3;
        }

        public class Event
        {
            public const int TitleMaxLength = 50;
            public const int TitleMinLength = 5;

            public const int DescriptionMaxLength = 1500;
            public const int DescriptionMinLength = 10;
        }

        public class Country
        {
            public const int NameMaxLength = 60;
            public const int NameMinLength = 4;

            public const int CodeLength = 2;
        }

        public class Region
        {
            public const int NameMaxLength = 20;
            public const int NameMinLength = 3;
        }

        public class Town
        {
            public const int NameMaxLength = 20;
            public const int NameMinLength = 3;
        }
    }
}