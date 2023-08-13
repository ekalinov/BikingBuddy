using BikingBuddy.Data;
using BikingBuddy.Data.Models;

namespace BikingBuddy.Services
{
    using Contracts;
    using Web.Models.Comment;

    using Microsoft.EntityFrameworkCore;
    public class CommentService : ICommentService
    {

        private readonly IEventService eventService;
        private readonly BikingBuddyDbContext dbContext;

        public CommentService(IEventService _eventService, BikingBuddyDbContext _dbContext)
        {
            eventService = _eventService;
            dbContext = _dbContext;
        }


        public async Task<ICollection<CommentViewModel>> GetAllComments(string eventId)
        {
            return await dbContext.Comments
                .Where(c => c.EventId == Guid.Parse(eventId) && !c.IsDeleted)
                .Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    CommentBody = c.CommentBody,
                    CreatedOn = c.CommentedOn,
                    UserImageURL = c.User.ProfileImageUrl,
                    UserName = c.User.Name, 
                    EditedOn = c.EditedOn, 
                    EventId = c.EventId.ToString(),
                    IsEdited = c.IsEdited
                })
                .OrderByDescending(c => c.CreatedOn)
                .ToListAsync();
        }

        public async Task AddComment(string comment, string userId, string eventId)
        {
            var eventToComment = await eventService.GetEventByIdAsync(eventId);

            if (eventToComment != null)
            {
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

        }


        public async Task EditComment(CommentViewModel commentModel)
        {
            Comment? commentToEdit = await GetCommentAsync(commentModel.Id);

            if (commentToEdit != null)
            {
                commentToEdit.IsEdited = true;
                commentToEdit.CommentedOn = DateTime.Now;
                commentToEdit.CommentBody = commentModel.CommentBody;

                await dbContext.SaveChangesAsync();
            }

        }


        private async Task<Comment?> GetCommentAsync(int commentId)
        {
            return await dbContext.Comments
                .Where(c => c.Id == commentId)
                .FirstOrDefaultAsync();
        }


        public async Task DeleteComment(int commentId)
        {
            Comment? commentToDelete = await GetCommentAsync(commentId);

            if (commentToDelete != null)
            {
                dbContext.Comments.Remove(commentToDelete);
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
