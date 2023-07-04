using BikingBuddy.Data;
using BikingBuddy.Data.Models;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Models.Comment;
using Microsoft.EntityFrameworkCore;

using static BikingBuddy.Common.ErrorMessages.CommentErrorMessages;


namespace BikingBuddy.Services
{
    public class CommentService : ICommentService
    {

        private readonly IEventService eventService;
        private readonly BikingBuddyDbContext dbContext;

        public CommentService(IEventService _eventService, BikingBuddyDbContext _dbContext)
        {
            this.eventService = _eventService;
            this.dbContext = _dbContext;
        }


        public async Task<ICollection<CommentViewModel>> GetAllComments(string eventId)
        {
            var eventComments = await dbContext.Comments
                .Where(c => c.EventId == Guid.Parse(eventId))
                .Select(c => new CommentViewModel()
                {
                    CommentBody = c.CommentBody, 
                    CreatedOn = c.CommentedOn,
                    UserImageURL = c.User.ImageURL,
                    UserName = c.User.UserName,
                    EditedOn = c.EditedOn,
                    IsEdited = c.IsEdited
                })
                .OrderByDescending(c => c.CreatedOn)
                .ToListAsync();

            


            return eventComments;
        }

        public async Task AddComment(string comment, string userId, string eventId)
        {
            var eventToComment = await eventService.GetEventByIdAsync(eventId);

            Comment newComment = new()
            {
                CommentBody = comment,
                CommentedOn = DateTime.Now,
                EventId = Guid.Parse(eventId),
                UserId = Guid.Parse(userId)
            };


            eventToComment.EventComments.Add(newComment);
            await dbContext.SaveChangesAsync();

        }

        
        public async Task EditComment(CommentViewModel commentModel)
        {
            Comment? commentToEdit = await dbContext.Comments
                .Where(c => c.Id == commentModel.Id)
                .FirstOrDefaultAsync();

            if (commentToEdit == null)
                throw new NullReferenceException(CommentDoesNotExist);

            commentToEdit.IsEdited = true;
            commentToEdit.CommentedOn = DateTime.Now;
            commentToEdit.CommentBody = commentModel.CommentBody;


            await dbContext.SaveChangesAsync();

        }



        public async Task DeleteComment(int commentId)
        {
            Comment? commentToDelete = await dbContext.Comments
                .Where(c => c.Id == commentId)
                .FirstOrDefaultAsync();

            if (commentToDelete == null)
                throw new NullReferenceException(CommentDoesNotExist);


            dbContext.Remove(commentToDelete);
            await dbContext.SaveChangesAsync();

        }
    }
}
