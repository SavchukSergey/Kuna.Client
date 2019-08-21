using Newtonsoft.Json;

namespace Kuna.Client {
    public class UserAsset {

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        [JsonProperty("locked")]
        public decimal Locked { get; set; }

   }
}