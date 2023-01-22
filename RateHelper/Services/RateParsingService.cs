using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RateHelper.Models;

namespace RateHelper.Services
{
    public class RateParsingService
    {
        private readonly HttpClient _httpClient;
        private readonly IRateService _rateService;
        
        public RateParsingService(HttpClient httpClient, IRateService rateService)
        {
            _httpClient = httpClient;
            _rateService = rateService;
        }

        public async void Scan()
        {
            while (true)
            {
                var rate = await GetOneRate();
                InsertRateToDb(rate);
                Thread.Sleep(60000);
            }
        }

        private async Task<YobitRate> GetOneRate()
        {
            var rateResponse = await _httpClient.GetAsync("https://yobit.net/api/3/ticker/eth_usd") 
                               ?? throw new Exception("yobit.net doesn't send data");
            var rateContent = await rateResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<YobitRate>(rateContent);
        }

        private async void InsertRateToDb(YobitRate rate)
        {
            await _rateService.CreateRate(
                new Rate
                {
                    RateName = "eth_usd",
                    Value = rate.EthUsd.Last,
                    UpdateDate = DateTime.Now,
                });
        }
    }
}