namespace CoffeeCompany.Models
{
    using System.ComponentModel.DataAnnotations;

    using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations.Schema;
    using MongoDB.Bson;

    public class Product
    {
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

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
