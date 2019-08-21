using System;
using System.Globalization;

namespace Kuna.Client.Utils {
    public static class UriUtils {

        public static Uri AppendQuery(this Uri uri, string key, decimal value) {
            return uri.AppendQuery(key, value.ToString(CultureInfo.InvariantCulture));
        }

        public static Uri AppendQuery(this Uri uri, string key, string value) {
            if (uri == null) {
                return null;
            }
            var str = $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}";
            var query = uri.Query;
            if (query.IndexOf('?') >= 0) {
                return new Uri(uri.OriginalString + "&" + str);
            } else {
                return new Uri(uri.OriginalString + "?" + str);
            }
        }

        public static Uri PrependQuery(this Uri uri, string key, string value) {
            if (uri == null) {
                return null;
            }
            var str = $"?{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}";
            var builder = new UriBuilder(uri);
            var query = builder.Query;
            if (!string.IsNullOrWhiteSpace(query)) {
                query = query.TrimStart('?');
                builder.Query = str + "&" + query;
            } else {
                builder.Query = str;
            }
            return builder.Uri;
        }

    }
}
