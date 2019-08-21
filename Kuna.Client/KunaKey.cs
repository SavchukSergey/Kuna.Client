using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Kuna.Client.Utils;

namespace Kuna.Client {
    public class KunaKey {

        public string PublicKey { get; }
        public string PrivateKey { get; }

        public KunaKey(string publicKey, string privateKey) {
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }

        public string GetSignature(HttpMethod method, Uri uri) {
            var query = uri.Query.TrimStart('?');
            var tbs = $"{method.ToString().ToUpperInvariant()}|{uri.LocalPath}|{query}";

            var key = Encoding.UTF8.GetBytes(PrivateKey);
            using (var alg = new HMACSHA256(key)) {
                var hash = alg.ComputeHash(Encoding.UTF8.GetBytes(tbs));
                return hash.ToHexString();
            }
        }

    }
}
