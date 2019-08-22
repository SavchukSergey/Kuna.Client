using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Kuna.Client {
    public class KunaQuery {

        private readonly string _url;
        private readonly SortedDictionary<string, string> _args = new SortedDictionary<string, string>();

        public Uri Uri {
            get {
                var query = new StringBuilder();
                foreach (var kv in _args) {
                    query.Append(query.Length > 0 ? '&' : '?');
                    query.Append(Uri.EscapeDataString(kv.Key));
                    query.Append('=');
                    query.Append(Uri.EscapeDataString(kv.Value));
                }
                var builder = new UriBuilder(_url);
                builder.Query = query.ToString();
                return builder.Uri;
            }
        }

        public KunaQuery(string url) {
            _url = url;
        }

        public KunaQuery AddQuery(string key, string value) {
            _args.Add(key, value);
            return this;
        }

        public KunaQuery AddQuery(string key, decimal value) {
            _args.Add(key, value.ToString(CultureInfo.InvariantCulture));
            return this;
        }

    }
}