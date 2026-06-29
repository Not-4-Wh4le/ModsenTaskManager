using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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

        public MongoDbContext(IOptions<MongoOptions> options)
        {
            var url = new MongoUrl(options.Value.ConnectionString);

            var client = new MongoClient(url);

            database = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
            => database.GetCollection<T>(collectionName);
    }
}
