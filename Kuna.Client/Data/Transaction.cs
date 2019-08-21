using System;
using Newtonsoft.Json;

namespace Kuna.Client {
    public class Transaction {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("volume")]
        public decimal Volume { get; set; }

        [JsonProperty("funds")]
        public decimal Funds { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("trend")]
        public string Trend { get; set; }

    }
}