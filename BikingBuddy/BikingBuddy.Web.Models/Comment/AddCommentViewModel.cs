using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static BikingBuddy.Common.EntityValidationsConstants.Comment;

namespace BikingBuddy.Web.Models.Comment
{
    public class AddCommentViewModel
    {
        [StringLength(CommentMaxLength, MinimumLength = CommentMinLength)]
        public string CommentBody { get; set; } = null!;
    }
}
    