using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeShopDemoRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> MongoCollection;

        public Repository(IConfiguration configuration)
        {
            MongoCollection = new MongoClient(configuration["MongoDb:ConnectionString"])
                             .GetDatabase(configuration["MongoDb:DbName"])
                             .GetCollection<T>(typeof(T).Name);
        }

        public async Task<IEnumerable<T>> Get()
        {
            var menuItems = await MongoCollection.FindAsync(menuItem => true);
            return menuItems.ToEnumerable();

        }


        public async Task Insert(T entity)
        {
             await MongoCollection.InsertOneAsync(entity);
        }

    }
}
