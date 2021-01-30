using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Kuna.Client.Utils;

namespace Kuna.Client {
    public class KunaKey {

        public string PublicKey { get; }
        public string PrivateKey { get; }

        private readonly byte[] _privateKeyBytes;

        public KunaKey(string publicKey, string privateKey) {
            if (publicKey == null) {
                throw new ArgumentNullException(nameof(publicKey));
            }
            if (privateKey == null) {
                throw new ArgumentNullException(nameof(privateKey));
            }
            PublicKey = publicKey;
            PrivateKey = privateKey;
            _privateKeyBytes = Encoding.UTF8.GetBytes(privateKey);
        }

        public string GetSignature(HttpRequestMessage requestMessage, string body = "{}") {
            var apiPath = requestMessage.RequestUri.AbsolutePath;
            var nonce = requestMessage.Headers.GetValues("kun-nonce").FirstOrDefault();
            using var alg = new HMACSHA384(_privateKeyBytes);
            var tbs = $"{apiPath}{nonce}{body}";
            var hash = alg.ComputeHash(Encoding.UTF8.GetBytes(tbs));
            return hash.ToHexString();
        }

    }
}
