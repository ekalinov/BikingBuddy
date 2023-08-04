namespace BikingBuddy.Web.Models.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string CommentBody { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string UserName { get; set; } = null!;

        public string? UserImageURL { get; set; } = null!;

        public bool IsEdited { get; set; } = false;

        public DateTime? EditedOn { get; set; }
    }
}