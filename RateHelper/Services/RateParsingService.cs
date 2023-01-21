using System.Net.Http;
using Newtonsoft.Json;
using RateHelper.Models;
using static System.String;

namespace RateHelper.Services
{
    public class RateParsingService
    {
        private readonly HttpClient _httpClient;
        
        public RateParsingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async void Scan()
        {
            var rateResponse = await _httpClient.GetAsync("https://yobit.net/api/3/ticker/eth_usd");
            var rateContent = rateResponse?.Content?.ToString() ?? Empty;
            var rate = JsonConvert.DeserializeObject<YobitRate>(rateContent);
        }
    }
}