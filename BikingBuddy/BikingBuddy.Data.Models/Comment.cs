namespace BikingBuddy.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        public int Id { get; set; }

        public Guid EventId { get; set; } 

        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; } = null!;

        public Guid UserId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public virtual AppUser User { get; set; } = null!;

        [Required]
        public DateTime CommentedOn { get; set; }
        
        [Required]
        public string CommentBody { get; set; } = null!;
        
        
        public bool IsDeleted { get; set; } = false;

        [Required]
        public bool IsEdited { get; set; } = false;

        public DateTime? EditedOn { get; set; }
    }
}
