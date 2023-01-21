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
                rateStoreDatabaseSettings.Value.RateCollectionName);
        }
        
        public Task<Rate> GetLastRate(DateTime updateDate)
        {
            return _rates
                       .AsQueryable()
                       .OrderBy(x => x.UpdateDate)
                       .FirstAsync(x => x.UpdateDate >= updateDate)
                   ?? throw new Exception($"Rate by date {updateDate} not found");
        }

        public Task<Rate> GetLastRate()
        {
            return _rates.AsQueryable().OrderByDescending(x => x.UpdateDate).FirstAsync()
                   ?? throw new Exception($"Rate not found");
        }
    }
}