namespace BikingBuddy.Services.Contracts
{
    using Web.Models.Comment;
    public interface ICommentService
    {
        Task<ICollection<CommentViewModel>> GetAllComments(string eventId);

        Task AddComment(string comment, string userId, string eventId);

        Task EditComment(CommentViewModel commentModel);

        Task DeleteComment(int commentId);
    }
}
