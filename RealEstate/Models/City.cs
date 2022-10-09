using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class City
    {
        public int CityId { get; set; } //primary key

        [Required]
        [MaxLength(50)]     
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string? AreaCode { get; set; }

        //child ref
        public List<ForSale>? ForSales { get; set; }
    }
}
