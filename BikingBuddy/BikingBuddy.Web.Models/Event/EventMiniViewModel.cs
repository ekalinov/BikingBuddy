namespace BikingBuddy.Web.Models.Event
{
    public class EventMiniViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Date { get; set; } = null!;

        public string Distance { get; set; } = null!;
         
        public string Ascent { get; set; } = null!;

        
        public string OrganizerUsername { get; set; } = null!;

        
        public string? Description { get; set; }
        public string Town { get; set; } = null!;
        
        public string? EventImageUrl { get; set; }

        public string ActivityType { get; set; } = null!;

      
    }
}