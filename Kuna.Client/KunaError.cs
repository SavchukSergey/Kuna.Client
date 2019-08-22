using Newtonsoft.Json;

namespace Kuna.Client {
    public class KunaError {

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

}