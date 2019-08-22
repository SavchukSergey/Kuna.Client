using System;

namespace Kuna.Client {
    public class KunaClientException : Exception {

        public KunaError Error { get; }

        public string Code => Error.Code;

        public KunaClientException(KunaError error) : base(error?.Message) {
            Error = error;
        }

    }
}