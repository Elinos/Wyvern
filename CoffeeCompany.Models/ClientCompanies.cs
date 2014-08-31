namespace CoffeeCompany.Models
{
    using System.ComponentModel.DataAnnotations;
    public class ClientCompanies
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public int CountryOfOrigin { get; set; }
    }
}
