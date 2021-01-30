using System;
using Kuna.Client.Serialization.Converters;
using Newtonsoft.Json;

namespace Kuna.Client {
    public class MarketData {

        [JsonProperty("at"), JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }

        [JsonProperty("ticker")]
        public MarketTickerData Ticker { get; set; }

    }
}