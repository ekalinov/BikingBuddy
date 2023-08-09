namespace BikingBuddy.Web.Models.User;

public class UserViewModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ProfileImageUrl { get; set; }


    public string? Country { get; set; }
    public string? Town { get; set; }

    public string? Helmet { get; set; }


    public string? Shoes { get; set; }


    public string? TeamName { get; set; }
    public string? TeamId { get; set; }

    public int? CompletedEvents { get; set; }

    public double TotalDistance { get; set; }

    public double TotalAscent { get; set; }
}