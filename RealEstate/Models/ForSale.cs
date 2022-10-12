using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class ForSale
    {
        public int ForSaleId { get; set; } //primary key

        [Required]
        [MaxLength(100)]    
        public string? Name { get; set; }

        [Required]
        //Range for prices
        [Range(50000,100000000, ErrorMessage = "This number isn't close to a House price")]
        //add the $ for the final price
        [DisplayFormat(DataFormatString = "{0:c}")]

        public double Price { get; set; }

        [Required]
        [MaxLength(2500)]
        public string? Description { get; set; }
        public string? Photo { get; set; } //photos

        [Required]
        public int Rooms { get; set; }

        [Required]
        [Display(Name = "Square Footage")]
        public int SqFootage { get; set; }

        //Parent Category
        [Display(Name = "City")]
        [Required]
        public int CityId { get; set; }

        [Required]
        //calls the City
        public City? City { get; set; }
    }
}
