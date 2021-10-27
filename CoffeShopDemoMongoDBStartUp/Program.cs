using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using CoffeShopDemoApi.Model;
using System.Collections.Generic;
using MongoDB.Bson;

namespace CoffeShopDemoMongoDBStartUp
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();

            var db = new MongoClient(config["MongoDb:ConnectionString"])
                             .GetDatabase(config["MongoDb:DbName"]);

            
            if( !db.ListCollectionNames().ToList().Contains(nameof(Order)))
            {
                db.CreateCollection(nameof(Order));
            }

            if (!db.ListCollectionNames().ToList().Contains(nameof(MenuItem)))
            {
                db.CreateCollection(nameof(MenuItem));
                //Now add some data
                string coffeeId = ObjectId.GenerateNewId().ToString();
                string teaId = ObjectId.GenerateNewId().ToString();
                string sandwitchId  = ObjectId.GenerateNewId().ToString();
                db.GetCollection<MenuItem>(nameof(MenuItem))
                   .InsertMany(new List<MenuItem> 
                   { 
                       new MenuItem { Id= coffeeId,  Name = "Coffee", Price =100 , Tax = 12 },
                       new MenuItem { Id= sandwitchId, Name = "Sandwitch", Price =70.23m , Tax = 5 },
                       new MenuItem {  Name = "Cold Coffee", Price =200 , Tax = 12, Discount=20, DiscountMenuItem= new List<string>() { sandwitchId },DiscountType = DiscountType.Percentage  },
                       new MenuItem { Id= teaId, Name = "Tea", Price =50 , Tax = 12 },
                       new MenuItem {  Name = "Cup Cake", Price = 25 , Tax = 5, Discount=15, DiscountMenuItem= new List<string>() { coffeeId, teaId},DiscountType = DiscountType.Flat }
                   });
            }
        }
    }
}
