using Newtonsoft.Json;

namespace Kuna.Client {
    public class MarketTickerData {

        [JsonProperty("buy")]
        public decimal Buy { get; set; }

        [JsonProperty("sell")]
        public decimal Sell { get; set; }

        [JsonProperty("low")]
        public decimal Low { get; set; }

        [JsonProperty("high")]
        public decimal High { get; set; }

        [JsonProperty("last")]
        public decimal Last { get; set; }

        [JsonProperty("vol")]
        public decimal Volume { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

    }
}