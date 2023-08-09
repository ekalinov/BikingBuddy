namespace BikingBuddy.Common
{
    public static class GlobalConstants
    {

        public const int ApplicationReleaseYear = 2023;
        public const int DefaultPage = 1;
        public const int EntitiesPerPage = 5;
 
        
        public const string UserRoleName = "User";
        
        public const string AdminRoleName = "Administrator";
        public const string AdminRoleEmail = "admin@bbuddy.com";

        public const string AdminAreaName = "Administration";


        public const long MaxPhotoSizeAllowed = 5242880;
        public const string MaxPhotoSizeAllowedErrorMessage =  "Error: Allowed photo size is up to 5Mb!";


        //Photo Storage paths
        public const string TeamPhotoDestinationPath = "FileStorage/TeamPhotos/";

        public const string TeamGalleryPhotosDestinationPath = "FileStorage/TeamPhotos/Gallery/";
        
        public const string EventPhotoDestinationPath = "FileStorage/EventPhotos/";

        public const string EventGalleryPhotosDestinationPath = "FileStorage/EventPhotos/Gallery/";

        public const string UserProfilePhotoDestinationPath = "FileStorage/UserPhotos/";
    }

    public static class DateTimeFormats
    {
        public const string DateFormat = "dd-MM-yyyy"; 

        public const string DateTimeFormat = "dd.MM.yyyy h:mmtt";
    }

    public static class NotificationMessagesConstants
    {

        public const string ErrorMessage = "ErrorMessage";

        public const string WarningMessage = "WarningMessage";

        public const string InformationMessage = "InfoMessage";

        public const string SuccessMessage = "SuccessMessage";

 
    }
    
    

}
