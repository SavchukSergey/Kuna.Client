using System;
using Newtonsoft.Json;

namespace Kuna.Client.Serialization.Converters {
    public class UserWalletConverter : JsonConverter<UserWallet> {

        public override UserWallet ReadJson(JsonReader reader, Type objectType, UserWallet existingValue, bool hasExistingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.StartArray) {

                var type = reader.ReadAsString();
                var currency = reader.ReadAsString();
                var balance = reader.ReadAsDecimal();
                var notUsed = reader.Read();
                var available = reader.ReadAsDecimal();

                while (reader.TokenType != JsonToken.EndArray) {
                    reader.Read();
                }

                return new UserWallet {
                    Currency = currency,
                    Balance = balance.Value,
                    AvailableBalance = available.Value
                };
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, UserWallet value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

    }
}