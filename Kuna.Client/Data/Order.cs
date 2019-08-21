using System;
using Newtonsoft.Json;

namespace Kuna.Client {
    public class Order {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("ord_type")]
        public string OrderType { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("avg_price")]
        public decimal AvgPrice { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("volume")]
        public decimal Volume { get; set; }

        [JsonProperty("remaining_volume")]
        public decimal RemainingVolume { get; set; }

        [JsonProperty("executed_volume")]
        public decimal ExecutedVolume { get; set; }

        [JsonProperty("trades_count")]
        public int TradesCount { get; set; }

    }
}