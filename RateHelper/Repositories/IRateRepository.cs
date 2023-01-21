using System;
using System.Threading.Tasks;
using RateHelper.Models;

namespace RateHelper.Repositories
{
    public interface IRateRepository
    {
        Task<Rate> GetLastRate(DateTime updateDate);
        Task<Rate> GetLastRate();
        Task CreateRate(Rate rate);
    }
}