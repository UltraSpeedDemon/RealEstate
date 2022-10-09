using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class City
    {
        public int CityId { get; set; }

        [Required]
        [MaxLength(50)]     
        public string? Name { get; set; }

        //child ref
        public List<ForSale> ForSale { get; set; }
    }
}
