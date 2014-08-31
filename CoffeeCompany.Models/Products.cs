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

        [Required]
        public decimal PricePerKgInDollars { get; set; }

        [Required]
        public CoffeeTypes TypeOfCoffee { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
