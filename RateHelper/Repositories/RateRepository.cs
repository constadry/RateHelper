using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RateHelper.Models;

namespace RateHelper.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly IMongoCollection<Rate> _rates;
        private const string _primaryPair = "eth_usd";
        
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
            return await _rates.Find(x => x.UpdateDate >= updateDate
                                          && x.RateName == _primaryPair)
                .SortBy(x => x.UpdateDate)
                .Limit(1)
                .SingleAsync();
        }

        public async Task<Rate> GetLastRate()
        {
            return await _rates.Find(x => x.RateName == _primaryPair)
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