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
            if (publicKey == null) {
                throw new ArgumentNullException(nameof(publicKey));
            }
            if (privateKey == null) {
                throw new ArgumentNullException(nameof(privateKey));
            }
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }

        public string GetSignature(HttpMethod method, KunaQuery query) {
            var uri = query.Uri;
            var args = uri.Query.TrimStart('?');
            var tbs = $"{method.ToString().ToUpperInvariant()}|{uri.LocalPath}|{args}";

            var key = Encoding.UTF8.GetBytes(PrivateKey);
            using (var alg = new HMACSHA256(key)) {
                var hash = alg.ComputeHash(Encoding.UTF8.GetBytes(tbs));
                return hash.ToHexString();
            }
        }

    }
}
