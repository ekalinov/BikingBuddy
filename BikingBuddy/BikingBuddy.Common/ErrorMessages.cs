namespace BikingBuddy.Common
{
    public static class ErrorMessages
    {
        public static class EventErrorMessages
        {
            public const string EventNotExistsMessage =  "Event does not exist!";

            public const string EventSuccessfullyEdited = "Event is successfully edited!";


            public const string EditEventError = "Something went wrong while Editing this event!";
            
            public const string UnauthorizedForError = "Error: Only Organisers of the event can edit or delete it!";

            
            public const string EventDeletedSuccessfully = "Event is successfully deleted!";

            public const string DeleteEventError = "Something went wrong while Deleting Event!";
             
            public const string EventAlreadyDeleted = "Event is alredy deleted!";



            public const string SuccessJoiningEvent = "Successfully join this event!";

            public const string UserAlreadyParticipatingErrorMessage = "You already joined this event!";

            public const string JoinEventError = "Something went wrong while joining this event!";

            public const string SuccessLeavingEvent = "You leave this event!";

            public const string LeaveEventError = "Something went wrong while leaving this event!";
            
            
            public const string CompletedEventSuccess = "You marked this event as completed!";

            public const string CompleteEventError = "Something went wrong while completeing event!";


            public const string UserNotParticipatingErrorMessage = "You are not participating in this event!";

            public const string UserDoesNotHaveEvents = "The user is not participating in any events!";

        }
        public static class BikeErrorMessages
        {
            public const string BikeAddedSuccessfully = "Bike successfully added to user! ";


            public const string AddBikeError = "Something went wrong while Adding a bike!";

            public const string EditBikeError = "Something went wrong! Edit Bike Error!";

            public const string BikeEditedSuccessfully = "Bike info is successfully edited!";

            public const string BikeRemovedFromUserSuccessfully = "Bike is successfully removed from user's bike collection!";

            public const string BikeRemovedFromUserError = "Something went wrong! Bike is not removed from User's bike collection!";


        }


        public static class UserErrorMessages
        {

            public const string UserNotFound = "User not found!";

            public const string UpdatingProfileError = "Something went wrong! Edit User Error!";

            public const string ProfileChangesSaved = "Profile changes saved!";
            
            public const string DeleteErrorUnauthorised = "Error: Only profile owners can delete their Profiles!";
             
            public const string UserAccountDeletedSuccessfully = "User Account is successfully deleted!";
             
            public const string DeleteProfileError = "Something went wrong! Delete User Account Error!";
            
            public const string EquipmentSuccessfully  = "Your equipment is successfully updated!";
            
            public const string EditEquipmentError  = "Something went wrong while Editing this Team info!";
            
            //UnAuthorize 
            public const string UnauthorizedErrorMessage = "Error: Only account owners and admins can make changes!";

            

        }

        public static class CommentErrorMessages
        {
            public const string CommentDoesNotExist = "Comment does not exist!"; 
            
            public const string CommentBoddyEmpty = "Error: Can't publish emtpy comment!";
            
            public const string NoCommentsForThisEventErrorMessage = "There is no comments, yet!";
            
            
            public const string CommentDeleteSuccessfully = "Comment Deleted!";
            
            public const string CommentDeleteError = "Error: Something went wrong deleting the comment!";

            public const string UnauthorisedDelete = "Error: Only Admins can delete comments!";


        }

        public static class TeamErrorMessages
        {
            //All Teams Messages

            public const string AllTeamsLoadingFail = "Something went wrong while loading all teams!";

            //UnAuthorize 
            public const string UnauthorizedErrorMessage = "Error: Only Team Managers can edit or delete team info!";


            
            //Add Team Messages

            public const string CountriesNotPreloaded = "Countries are not preloaded!";


            public const string TeamDoesNotExist = "Team does not exist!";
            
            public const string TeamAddedSuccessfully = "Team added successfully!";
            
            public const string TeamDeletedSuccessfully = "Team is successfully deleted!";


            public const string DeleteTeamError = "Something went wrong while Deleting Team!";
            
            public const string AlreadyDeleted = "Team is alredy deleted!";

            public const string AddTeamError = "Something went wrong while Adding Team!";



            //Edit Team Messages

            public const string EditTeamError = "Something went wrong while Editing this Team info!";

            public const string TeamEditedSuccessfully = "Team info is updated successfully!";


            //User Messages 
            
            public const string UserIsNotAMember = "The user is not a member of this team!";

            public const string UserAlreadyMember = "User is already a member of that team!";
 



            //Request Messages
            public const string RequestAlreadySend = "You have panding request!";

            public const string RequestSend = "Request is send! Your application should be reviewed by the team manager!";

            public const string RequestRejected = "Member request was rejected successfully!";

            public const string RequestNotFound = "Request not found!";


            //Add/Remove Member Messages
            public const string MemberAddedSuccessfully = "Member is successfully added to the team!";

            public const string MemberRemovedSuccessfully = "Member is successfully removed from the team!";


            public const string AddMemberError = "Something went wrong while adding the member to the team!";

            public const string RemoveMemberError = "Something went wrong while removing the member to the team!";

        }

    }
}
