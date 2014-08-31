namespace CoffeeCompany.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson;

    public class ClientCompany
    {
        private ICollection<Order> orders;
        private ICollection<Product> products;

        public ClientCompany()
        {
            this.orders = new HashSet<Order>();
            this.products = new HashSet<Product>();
        }

        [BsonIgnore]
        public int ID { get; set; }

        [BsonId]
        [NotMapped]
        public ObjectId MongoId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }

        [BsonIgnore]
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
