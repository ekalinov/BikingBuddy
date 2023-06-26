using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Web.Models.Comment
{
    public class CommentViewModel
    {

        public string UserName { get; set; } = null!;

        public string CommentBody { get; set; } = null!;

        public DateTime CommentedOn { get; set; } 
    }
}
