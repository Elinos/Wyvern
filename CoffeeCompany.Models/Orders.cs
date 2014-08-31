namespace CoffeeCompany.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        private ICollection<Product> products;

        public Order()
        {
            this.products = new HashSet<Product>();
        }

        public int ID { get; set; }

        [Required]
        public int QuantityInKg { get; set; }

        [NotMapped]
        public decimal OrderTotalPrice { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        //[Required]
        public virtual ICollection<Product> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.products = value;
            }
        }

        public int ClientCompanyId { get; set; }

        public virtual ClientCompany ClientCompany { get; set; }
    }
}
