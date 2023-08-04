namespace BikingBuddy.Web.Models.Comment
{
    public class CommentSectionViewModel
    {
        public CommentSectionViewModel()
        {
            AllComments = new HashSet<CommentViewModel>();
        }

        public string EventId { get; set; } = null!;

        public ICollection<CommentViewModel> AllComments { get; set; }

        public string CommentBody { get; set; } = null!;
    }
}