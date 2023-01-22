namespace RateHelper.Models
{
    using Newtonsoft.Json;

    public partial class YobitRate
    {
        [JsonProperty("eth_usd", NullValueHandling = NullValueHandling.Ignore)]
        public EthUsd EthUsd { get; set; }
    }

    public partial class EthUsd
    {
        [JsonProperty("last", NullValueHandling = NullValueHandling.Ignore)]
        public double? Last { get; set; }
    }
}