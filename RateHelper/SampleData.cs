using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RateHelper.Models;

namespace RateHelper
{
    public class SampleData
    {
        public static void Initialize(IOptions<RateStoreDatabaseSettings> rateStoreDatabaseSettings)
        {
            var mongoClient = 
                new MongoClient(rateStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase =
                mongoClient.GetDatabase(rateStoreDatabaseSettings.Value.DatabaseName);
            var rates = mongoDatabase.GetCollection<Rate>(
                rateStoreDatabaseSettings.Value.RateCollectionName);
            
            var data = new List<Rate>
            {
                new Rate
                {
                    Name = "eth_usd",
                    UpdateDate = DateTime.Now,
                    Value = 123213.23213,
                },
                new Rate
                {
                    Name = "eth_usd",
                    UpdateDate = DateTime.Now,
                    Value = 123213.14252,
                }
            };
            
            rates.InsertMany(data);
        }
    }
}