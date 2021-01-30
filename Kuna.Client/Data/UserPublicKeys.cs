using Newtonsoft.Json;

namespace Kuna.Client {
    public class UserPublicKeys {

        [JsonProperty("deposit_sdk_uah_public_key")]
        public string DepositSdkUahPublicKey { get; set; }

        //todo: other fields

    }
}