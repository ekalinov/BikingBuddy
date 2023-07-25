using System.ComponentModel.DataAnnotations;
using static BikingBuddy.Common.EntityValidationsConstants.Comment;

namespace BikingBuddy.Web.Models.Comment
{
    public class AddCommentViewModel
    {
        [StringLength(CommentMaxLength, MinimumLength = CommentMinLength)]
        public string CommentBody { get; set; } = null!;
    }
}
    