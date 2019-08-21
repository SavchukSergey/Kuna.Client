using System;
using Kuna.Client.Utils;
using Newtonsoft.Json;

namespace Kuna.Client.Converters {
    public class UnixDateTimeConverter : JsonConverter<DateTime> {

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer) {
            var val = reader.Value;
            if (val == null || val.Equals("")) {
                return DateTime.MinValue;
            }
            if (val is string str) {
                var value = long.Parse(str);
                return DateTimeUtils.FromUnixTimestamp(value);
            }
            if (val is long longValue) {
                return DateTimeUtils.FromUnixTimestamp(longValue);
            }
            return DateTime.MinValue;
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer) {
            writer.WriteValue(DateTimeUtils.ToUnixTimestamp(value));
        }

    }
}