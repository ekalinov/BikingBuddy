using BikingBuddy.Web.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Services.Contracts
{
    public interface ICommentService
    {
        Task<ICollection<CommentViewModel>> GetAllComments(string eventId);

        Task AddComment(string comment, string userId, string eventId);

        Task EditComment(CommentViewModel commentModel);

        Task DeleteComment(int commentId);
    }
}
