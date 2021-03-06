﻿namespace CoffeeCompany.Models
{
    using System.ComponentModel.DataAnnotations;

    using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations.Schema;
    using MongoDB.Bson;
    using System.Collections.Generic;

    public class Product
    {
        private ICollection<Order> orders;

        public Product()
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
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal PricePerKgInDollars { get; set; }

        [Required]
        public CoffeeTypes TypeOfCoffee { get; set; }

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
