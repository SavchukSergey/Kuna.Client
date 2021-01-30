using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kuna.Client {
    public class KunaMarketAuthClient : KunaBaseClient {

        private readonly string _market;
        private readonly KunaKey _key;

        public KunaMarketAuthClient(KunaClient transport, string market, KunaKey key) : base(transport) {
            if (key == null) {
                throw new ArgumentNullException(nameof(key));
            }
            _market = market;
            _key = key;
        }

        // public Task<MarketData> GetMarketDataAsync() {
        //     return _transport.GetMarketDataAsync(_market);
        // }

        // public Task<OrderBook> GetOrderBookAsync() {
        //     return _transport.GetOrderBookAsync(_market);
        // }

        // public Task<IList<Transaction>> GetTradeHistoryAsync() {
        //     return _transport.GetTradeHistoryAsync(_market);
        // }

        public Task<UserInfo> GetMeAsync() {
            return _transport.GetMeAsync(_key);
        }

        // public Task<IList<Transaction>> GetMyTradesAsync() {
        //     return _transport.GetMyTradesAsync(_market, _key);
        // }

        // public Task<Order> PlaceOrderAsync(string side, decimal volume, decimal price) {
        //     return _transport.PlaceOrderAsync(side, volume, _market, price, _key);
        // }

        // public Task<Order> PlaceBuyOrderAsync(decimal volume, decimal price) {
        //     return _transport.PlaceBuyOrderAsync(volume, _market, price, _key);
        // }

        // public Task<Order> PlaceSellOrderAsync(decimal volume, decimal price) {
        //     return _transport.PlaceSellOrderAsync(volume, _market, price, _key);
        // }

        // public Task<Order> CancelOrderAsync(string orderId) {
        //     return _transport.CancelOrderAsync(orderId, _key);
        // }

        // public Task<IList<Order>> GetActiveOrderAsync() {
        //     return _transport.GetActiveOrderAsync(_market, _key);
        // }
    }
}