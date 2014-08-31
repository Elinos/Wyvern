namespace CoffeeCompany.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Product
    {
        public int ID { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

        // In dollars
        public decimal PricePerKgInDollars { get; set; }

        public CoffeeTypes TypeOfCoffee { get; set; }
    }
}
