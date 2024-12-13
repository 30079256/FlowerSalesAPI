using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowerSales.API.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string StoreLocation { get; set; }

        [Required]
        public int PostCode { get; set; }

        [Required]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
