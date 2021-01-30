using System;
using Newtonsoft.Json;

namespace Kuna.Client.Serialization.Converters {
    public class OrderAskConverter : JsonConverter<OrderAsk> {

        public override OrderAsk ReadJson(JsonReader reader, Type objectType, OrderAsk existingValue, bool hasExistingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.StartArray) {

                var price = reader.ReadAsDecimal() ?? 0.0m;
                var amount = reader.ReadAsDecimal() ?? 0.0m;

                reader.Skip();
                reader.Read();

                return new OrderAsk {
                    Price = price,
                    Amount = amount
                };

            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, OrderAsk value, JsonSerializer serializer) {
            writer.WriteValue(1);
        }

    }
}