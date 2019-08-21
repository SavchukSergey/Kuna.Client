using Newtonsoft.Json;

namespace Kuna.Client {
    public class UserInfo {

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("activated")]
        public bool Activated { get; set; }

        [JsonProperty("accounts")]
        public UserAsset[] Assets { get; set; }

   }
}