namespace BikingBuddy.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    using static BikingBuddy.Common.EntityValidationsConstants.Town;

    public class Town
    {

        public Town()
        {
            TownEvents = new HashSet<Event>();
            TownUsers = new HashSet<AppUser>();
        }
        

        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(TownNameMaxLength)]
        public string Name { get; set; } = null!;


        public ICollection<Event> TownEvents { get; set; } 


        public ICollection<AppUser> TownUsers { get; set; } 
    }
}
