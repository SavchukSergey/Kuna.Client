using System;
using Kuna.Client.Serialization.Converters;
using Newtonsoft.Json;

namespace Kuna.Client {
    public class OrderBook {

        [JsonProperty("timestamp"), JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }

        [JsonProperty("asks", ItemConverterType = typeof(OrderAskConverter))]
        public OrderAsk[] Asks { get; set; }

        [JsonProperty("bids", ItemConverterType = typeof(OrderBidConverter))]
        public OrderBid[] Bids { get; set; }

    }
}