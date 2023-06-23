namespace BikingBuddy.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static BikingBuddy.Common.EntityValidationsConstants.Country;


    public class Country
    {

        public Country()
        {
           this.CountryEvents = new HashSet<Event>();
        }

        [Key]
        [MaxLength(CodeLength)]
        public string Code { get; set; } = null!;

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;




        public ICollection<Event> CountryEvents { get; set; } 

    }
}
