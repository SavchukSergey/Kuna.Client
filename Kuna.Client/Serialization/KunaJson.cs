using Newtonsoft.Json;
using Kuna.Client.Serialization.Converters;

namespace Kuna.Client.Serialization {
    public static class KunaJson {

        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings {
            Converters = {
                new UserWalletConverter()
            }
        };

        public static TResult Deserialize<TResult>(string json) {
            return JsonConvert.DeserializeObject<TResult>(json, _settings);
        }

        public static string Serialize<TResult>(TResult obj) {
            return JsonConvert.SerializeObject(obj, _settings);
        }

    }
}