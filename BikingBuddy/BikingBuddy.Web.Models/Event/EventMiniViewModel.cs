
namespace BikingBuddy.Web.Models.Event
{
	public class EventMiniViewModel
	{
		public string Id { get; set; } = null!;

		public string Title { get; set; } = null!;

		public string Date { get; set; } = null!;

		public string? EventImageUrl { get; set; }   

		public string ActivityType { get; set; } = null!;
		
		public string? Description { get; set; }
	}
}
