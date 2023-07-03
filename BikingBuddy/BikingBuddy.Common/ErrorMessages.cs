using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Common
{
   public static class ErrorMessages
    {
        public static class EventErrorMessages
        {
            public const string EventNotExistsMessage =  "Event does not exist!";

            public const string UserAlreadyParticipatingErrorMessage = "The user is already joined in this event!";

            public const string UserNotParticipatingErrorMessage = "The user is not participating in this event!";

            public const string UserDoesNotHaveEvents = "The user is not participating in any events!";

        }


        public static class CommentErrorMessages
        {
            public const string CommentDoesNotExist = "Comment does not exist!";

            public const string NoCommentsForThisEventErrorMessage = "There is no comments, yet!";


        }

        public static class TeamErrorMessages
        {
            public const string TeamDoesNotExist = "Team does not exist!";

            public const string UserDoesNotExist = "Invalid User!";

            public const string UserAlreadyAMember = "The user is already a member!";

            public const string UserIsNotAMember = "The user is not a member of this team!";



        }

    }
}
