using System;
using System.Collections.Generic;
using System.Linq;
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
        private const int _oneSecond = 1000;
        
        public RateParsingService(HttpClient httpClient, IRateService rateService)
        {
            _httpClient = httpClient;
            _rateService = rateService;
        }

        public async void Scan()
        {
            const int timeout = 60000;
            const string primaryPair = "eth_usd";
            var pairNames = await GetPairNames();
            var rnd = new Random();

            while (true)
            {
                // primary ticker
                var rateEthUsd = await GetOneRate(primaryPair);
                InsertRateToDb(rateEthUsd, primaryPair);

                Thread.Sleep(_oneSecond);
                
                // other ticker (getting randomly to don't create a high load on api)
                var tickerInd = rnd.Next(pairNames.Count - 1);
                var tickerName = pairNames.ElementAtOrDefault(tickerInd);
                var rate = await GetOneRate(tickerName);
                InsertRateToDb(rate, tickerName);
                Thread.Sleep(timeout);
            }
        }

        private async Task<List<string>> GetPairNames()
        {
            var response = await _httpClient.GetAsync("https://yobit.net/api/3/info")
                           ?? throw new Exception("yobit.net doesn't send data");
            var json = await response.Content.ReadAsStringAsync();
            var o = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            var pairs = JsonConvert.DeserializeObject<Dictionary<string, object>>(o["pairs"].ToString());
            Thread.Sleep(_oneSecond);
            return pairs.Keys.ToList();
        }

        private async Task<dynamic> GetOneRate(string tickerName)
        {
            var rateResponse = await _httpClient.GetAsync($"https://yobit.net/api/3/ticker/{tickerName}") 
                               ?? throw new Exception("yobit.net doesn't send data");
            var rateContent = await rateResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(rateContent);
        }

        private async void InsertRateToDb(dynamic rate, string tickerName)
        {
            await _rateService.CreateRate(
                new Rate
                {
                    RateName = tickerName,
                    Value = (double)rate[tickerName]["last"],
                    UpdateDate = DateTime.Now,
                });
        }
    }
}