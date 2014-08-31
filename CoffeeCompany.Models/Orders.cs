namespace CoffeeCompany.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        private ICollection<Product> products;

        public Order()
        {
            this.products = new HashSet<Product>();
        }
        public int ID { get; set; }
        public int QuantityInKg { get; set; }

        // In dollars
        public decimal OrderTotalPrice { get; set; }

        public OrderStatus Status { get; set; }

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
    }
}
