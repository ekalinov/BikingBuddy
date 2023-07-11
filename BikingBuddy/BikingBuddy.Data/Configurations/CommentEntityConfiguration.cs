namespace BikingBuddy.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Models;
    internal class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {

            builder
                .HasOne(e => e.Event)
                .WithMany(e => e.EventComments)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
