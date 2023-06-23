namespace BikingBuddy.Data.Models
{

    using System.ComponentModel.DataAnnotations;

    using static BikingBuddy.Common.EntityValidationsConstants.Town;

    public class Town
    {

        public Town()
        {
            this.TownEvents = new HashSet<TownEvent>();
        }
        

        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;


        public ICollection<TownEvent> TownEvents { get; set; } 
    }
}
