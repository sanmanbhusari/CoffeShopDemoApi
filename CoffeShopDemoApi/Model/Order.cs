using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeShopDemoApi.Model
{
    
    public class Order
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]

        public IEnumerable<MenuItem> MenuItems { get; set; }

    }
}
