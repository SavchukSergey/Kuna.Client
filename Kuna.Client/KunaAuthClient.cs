using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kuna.Client {
    public class KunaAuthClient : KunaBaseClient {

        private readonly KunaKey _key;

        public KunaAuthClient(KunaClient transport, KunaKey key) : base(transport) {
            if (key == null) {
                throw new ArgumentNullException(nameof(key));
            }
            _key = key;
        }

        public KunaMarketAuthClient UseMarket(string market) {
            return new KunaMarketAuthClient(_transport, market, _key);
        }

        public Task<MarketData> GetMarketDataAsync(string market) {
            return _transport.GetMarketDataAsync(market);
        }

        public Task<OrderBook> GetOrderBookAsync(string market) {
            return _transport.GetOrderBookAsync(market);
        }

        public Task<IList<Transaction>> GetTradeHistoryAsync(string market) {
            return _transport.GetTradeHistoryAsync(market);
        }

        public Task<UserInfo> GetMeAsync() {
            return _transport.GetMeAsync(_key);
        }

        public Task<IList<Transaction>> GetMyTradesAsync(string market) {
            return _transport.GetMyTradesAsync(market, _key);
        }

        public Task<Order> PlaceOrderAsync(string side, decimal volume, string market, decimal price) {
            return _transport.PlaceOrderAsync(side, volume, market, price, _key);
        }

        public Task<Order> PlaceBuyOrderAsync(decimal volume, string market, decimal price) {
            return _transport.PlaceBuyOrderAsync(volume, market, price, _key);
        }

        public Task<Order> PlaceSellOrderAsync(decimal volume, string market, decimal price) {
            return _transport.PlaceSellOrderAsync(volume, market, price, _key);
        }

        public Task<Order> CancelOrderAsync(string orderId) {
            return _transport.CancelOrderAsync(orderId, _key);
        }

        public Task<IList<Order>> GetActiveOrderAsync(string market) {
            return _transport.GetActiveOrderAsync(market, _key);
        }
    }
}
