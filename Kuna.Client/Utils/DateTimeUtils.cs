using System;

namespace Kuna.Client.Utils {
    public static class DateTimeUtils {

        private static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        public static long ToUnixTimestamp(DateTime date) {
            return (long)(date.Subtract(_unixEpoch).TotalSeconds);
        }

        public static long ToUnixTimestampMillis(DateTime date) {
            return (long)(date.Subtract(_unixEpoch).TotalMilliseconds);
        }

        public static DateTime FromUnixTimestamp(long timestamp) {
            return _unixEpoch.AddSeconds(timestamp);
        }

    }
}
