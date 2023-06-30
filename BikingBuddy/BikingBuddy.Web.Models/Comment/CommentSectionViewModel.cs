using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Web.Models.Comment
{
    public class CommentSectionViewModel
    {

        public CommentSectionViewModel()
        {
            this.AllComments = new HashSet<CommentViewModel>();
        }

        public string EventId { get; set; } = null!; 

        public ICollection<CommentViewModel> AllComments { get; set; }

        public string CommentBody { get; set; } = null!;
    }
}
