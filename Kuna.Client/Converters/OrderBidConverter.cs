using System;
using Newtonsoft.Json;

namespace Kuna.Client.Converters {
    public class OrderBidConverter : JsonConverter<OrderBid> {

        public override OrderBid ReadJson(JsonReader reader, Type objectType, OrderBid existingValue, bool hasExistingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.StartArray) {

                var price = reader.ReadAsDecimal() ?? 0.0m;
                var amount = reader.ReadAsDecimal() ?? 0.0m;

                reader.Skip();
                reader.Read();

                return new OrderBid {
                    Price = price,
                    Amount = amount
                };
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, OrderBid value, JsonSerializer serializer) {
            writer.WriteValue(1);
        }

    }
}