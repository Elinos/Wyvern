namespace CoffeeCompany.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Employee
    {
        private ICollection<Order> orders;

        public Employee()
        {
            this.orders = new HashSet<Order>();
        }

        [BsonIgnore]
        public int ID { get; set; }

        [BsonId]
        [NotMapped]
        public ObjectId MongoId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Username { get; set; }

        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

        [BsonIgnore]
        public virtual ICollection<Order> Orders 
        { 
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
