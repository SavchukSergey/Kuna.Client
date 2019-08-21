using System;
using System.Threading.Tasks;

namespace Kuna.Client {
    public abstract class KunaBaseClient {

        protected readonly KunaClient _transport;

        protected KunaBaseClient(KunaClient transport) {
            _transport = transport;
        }

        public Task<DateTime> GetTimestampAsync() {
            return _transport.GetTimestampAsync();
        }

    }
}
