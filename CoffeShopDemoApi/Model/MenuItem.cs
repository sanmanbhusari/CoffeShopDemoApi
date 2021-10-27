using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CoffeShopDemoApi.Model
{
    public class MenuItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Tax { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public decimal Discount { get; set; }

        public DiscountType DiscountType { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public IEnumerable<string> DiscountMenuItem { get; set; }
    }
}
