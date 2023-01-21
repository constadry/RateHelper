using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using RateHelper.Models;

namespace RateHelper.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly IMongoCollection<Rate> _rates;
        
        public RateRepository(IOptions<RateStoreDatabaseSettings> rateStoreDatabaseSettings)
        {
            var mongoClient = 
                new MongoClient(rateStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase =
                mongoClient.GetDatabase(rateStoreDatabaseSettings.Value.DatabaseName);
            _rates = mongoDatabase.GetCollection<Rate>(
                rateStoreDatabaseSettings.Value.RatesCollectionName);
        }
        
        public async Task<Rate> GetLastRate(DateTime updateDate)
        {
            // var filter = Builders<Rate>.Filter.Gte(x => x.UpdateDate, updateDate);
            return await _rates.Find(x => x.UpdateDate >= updateDate)
                .SortBy(x => x.UpdateDate)
                .Limit(1)
                .SingleAsync();
        }

        public async Task<Rate> GetLastRate()
        {
            return await _rates.Find(x => true)
                .SortByDescending(x => x.UpdateDate)
                .Limit(1)
                .SingleAsync();
        }

        public async Task CreateRate(Rate rate)
        {
            await _rates.InsertOneAsync(rate);
        }
    }
}