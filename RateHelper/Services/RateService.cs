using System;
using System.Threading.Tasks;
using RateHelper.Models;
using RateHelper.Models.Responses;
using RateHelper.Repositories;

namespace RateHelper.Services
{
    public class RateService : IRateService
    {
        private readonly IRateRepository _rateRepository;

        public RateService(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public async Task<RateResponse> GetLastRate(DateTime updateDate)
        {
            try
            {
                var entity = await _rateRepository.GetLastRate(updateDate);
                return new RateResponse(entity);
            }
            catch (Exception ex)
            {
                return new RateResponse($"An error occurred when finding the rate: {ex.Message}");
            }
        }

        public async Task<RateResponse> GetLastRate()
        {
            try
            {
                var entity = await _rateRepository.GetLastRate();
                return new RateResponse(entity);
            }
            catch (Exception ex)
            {
                return new RateResponse($"An error occurred when finding the rate: {ex.Message}");
            }
        }

        public async Task CreateRate(Rate rate)
        {
            await _rateRepository.CreateRate(rate);
        }
    }
}