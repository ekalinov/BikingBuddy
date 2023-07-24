namespace BikingBuddy.Common
{
    public class EntityValidationsConstants
    {
        public class User
        {
            public const int UserNameMaxLength = 30;
            public const int UserNameMinLength = 5;

            public const int UsernameMaxLength = 10;
            public const int UsernameMinLength = 4;
        }

        public class ActivityType
        {
            public const int ActivityTypeNameMaxLength = 15;
            public const int ActivityTypeNameMinLength = 3;
        }

        public class Bike
        {
            public const int FrameBrandMaxLength = 20;
            public const int FrameBrandMinLength = 2;


            public const int WheelBrandMaxLength = 20;
            public const int WheelBrandMinLength = 2;

            public const int FrameSizeMaxLength = 2;
            public const int FrameSizeMinLength = 1;

            public const int DrivetrainMaxLength = 20;
            public const int DrivetrainMinLength = 2;


            public const int ForkMaxLength = 20;
            public const int ForkMinLength = 2;
        }

        public class BikeType
        {
            public const int BikeTypeNameMaxLength = 20;
            public const int BikeTypeNameMinLength = 2;
        }



        public class Comment
        {
            public const int CommentMaxLength = 255;
            public const int CommentMinLength = 1;
        }

        public class Ride
        {
            public const int RideNameMaxLength = 50;
            public const int RideNameMinLength = 3;
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

        public class Municipality
        {
            public const int MunicipalityNameMaxLength = 20;
            public const int MunicipalityNameMinLength = 3;
        }

        public class Team
        {
            public const int TeamNameMaxLength = 30;
            public const int TeamNameMinLength = 2;


            public const int TeamDescriptionMaxLength = 300;
            public const int TeamDescriptionMinLength = 15;
        }

        public class Town
        {
            public const int TownNameMaxLength = 20;
            public const int TownNameMinLength = 3;
        }
    }
}