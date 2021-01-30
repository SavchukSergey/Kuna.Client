using Newtonsoft.Json;

namespace Kuna.Client {
    public class ServerTime {

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("timestamp_miliseconds")]
        public long TimestampMilliseconds { get; set; }

   }
}