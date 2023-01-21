using System;
using System.Threading.Tasks;
using RateHelper.Models;
using RateHelper.Models.Responses;

namespace RateHelper.Services
{
    public interface IRateService
    {
        Task<RateResponse> GetLastRate(DateTime updateDate);
        Task<RateResponse> GetLastRate();
    }
}