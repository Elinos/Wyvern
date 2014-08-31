namespace CoffeeCompany.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ClientCompany
    {
        private ICollection<Order> orders;
        public ClientCompany()
        {
            this.orders = new HashSet<Order>();
        }
        public int ID { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]

        public string Name { get; set; }
        public int CountryOfOrigin { get; set; }

        public virtual ICollection<Order> Orders { 
            get
            {
                return this.orders;
            }
            set
            {
                this.orders = value;
            }
        }
    }
}
