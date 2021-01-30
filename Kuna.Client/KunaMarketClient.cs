using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kuna.Client {
    public class KunaMarketClient : KunaBaseClient {

        private readonly string _market;

        public KunaMarketClient(KunaClient transport, string market) : base(transport) {
            _market = market;
        }

        public KunaMarketAuthClient UseKey(KunaKey key) {
            return new KunaMarketAuthClient(_transport, _market, key);
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

        public Task<UserInfo> GetMeAsync(KunaKey key) {
            return _transport.GetMeAsync(key);
        }

        // public Task<IList<Transaction>> GetMyTradesAsync(KunaKey key) {
        //     return _transport.GetMyTradesAsync(_market, key);
        // }

        // public Task<Order> PlaceOrderAsync(string side, decimal volume, decimal price, KunaKey key) {
        //     return _transport.PlaceOrderAsync(side, volume, _market, price, key);
        // }

        // public Task<Order> PlaceBuyOrderAsync(decimal volume, decimal price, KunaKey key) {
        //     return _transport.PlaceBuyOrderAsync(volume, _market, price, key);
        // }

        // public Task<Order> PlaceSellOrderAsync(decimal volume, decimal price, KunaKey key) {
        //     return _transport.PlaceSellOrderAsync(volume, _market, price, key);
        // }

        // public Task<Order> CancelOrderAsync(string orderId, KunaKey key) {
        //     return _transport.CancelOrderAsync(orderId, key);
        // }

        // public Task<IList<Order>> GetActiveOrderAsync(KunaKey key) {
        //     return _transport.GetActiveOrderAsync(_market, key);
        // }

    }
}
