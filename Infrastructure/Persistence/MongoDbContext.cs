using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Persistence
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");

            var url = new MongoUrl(connectionString);

            var client = new MongoClient(url);

            database = client.GetDatabase(url.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
            => database.GetCollection<T>(collectionName);
    }
}
