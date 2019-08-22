using Newtonsoft.Json;

namespace Kuna.Client {
    public class KunaErrorMessage {

        [JsonProperty("error")]
        public KunaError Error { get; set; }

    }

}