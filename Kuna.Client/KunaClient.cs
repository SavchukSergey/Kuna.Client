using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Kuna.Client.Utils;
using Newtonsoft.Json;

namespace Kuna.Client {
    public class KunaClient {

        private static readonly HttpClient _client = new HttpClient();

        public KunaAuthClient UseKey(KunaKey key) {
            return new KunaAuthClient(this, key);
        }

        public KunaMarketClient UseMarket(string market) {
            return new KunaMarketClient(this, market);
        }

        public async Task<DateTime> GetTimestampAsync() {
            using (var response = await _client.GetAsync("https://kuna.io/api/v2/timestamp").ConfigureAwait(false)) {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var timestamp = long.Parse(content);
                return DateTimeUtils.FromUnixTimestamp(timestamp);
            }
        }

        public Task<MarketData> GetMarketDataAsync(string market) {
            return FetchJsonGetAsync<MarketData>(new KunaQuery($"https://kuna.io/api/v2/tickers/{market}"));
        }

        public Task<OrderBook> GetOrderBookAsync(string market) {
            return FetchJsonGetAsync<OrderBook>(new KunaQuery($"https://kuna.io/api/v2/depth?market={market}"));
        }

        public Task<IList<Transaction>> GetTradeHistoryAsync(string market) {
            var query = new KunaQuery("https://kuna.io/api/v2/trades")
                .AddQuery("market", market);
            return FetchJsonGetAsync<IList<Transaction>>(query);
        }

        public Task<UserInfo> GetMeAsync(KunaKey key) {
            var query = new KunaQuery("https://kuna.io/api/v2/members/me");
            return FetchAuthJsonGetAsync<UserInfo>(query, key);
        }

        public Task<IList<Transaction>> GetMyTradesAsync(string market, KunaKey key) {
            var query = new KunaQuery("https://kuna.io/api/v2/trades/my")
                .AddQuery("market", market);
            return FetchAuthJsonGetAsync<IList<Transaction>>(query, key);
        }

        public Task<Order> PlaceOrderAsync(string side, decimal volume, string market, decimal price, KunaKey key) {
            var query = new KunaQuery("https://kuna.io/api/v2/orders")
                .AddQuery("side", side)
                .AddQuery("volume", volume)
                .AddQuery("market", market)
                .AddQuery("price", price);
            return FetchAuthJsonAsync<Order>(HttpMethod.Post, query, key);
        }

        public Task<Order> PlaceBuyOrderAsync(decimal volume, string market, decimal price, KunaKey key) {
            var query = new KunaQuery("https://kuna.io/api/v2/orders")
                .AddQuery("side", "buy")
                .AddQuery("volume", volume)
                .AddQuery("market", market)
                .AddQuery("price", price);
            return FetchAuthJsonAsync<Order>(HttpMethod.Post, query, key);
        }

        public Task<Order> PlaceSellOrderAsync(decimal volume, string market, decimal price, KunaKey key) {
            var query = new KunaQuery("https://kuna.io/api/v2/orders")
                .AddQuery("side", "sell")
                .AddQuery("volume", volume)
                .AddQuery("market", market)
                .AddQuery("price", price);
            return FetchAuthJsonAsync<Order>(HttpMethod.Post, query, key);
        }

        public Task<Order> CancelOrderAsync(string orderId, KunaKey key) {
            var query = new KunaQuery("https://kuna.io/api/v2/order/delete")
                .AddQuery("id", orderId);
            return FetchAuthJsonAsync<Order>(HttpMethod.Post, query, key);
        }

        public Task<IList<Order>> GetActiveOrderAsync(string market, KunaKey key) {
            var query = new KunaQuery("https://kuna.io/api/v2/orders")
                .AddQuery("market", market);
            return FetchAuthJsonGetAsync<IList<Order>>(query, key);
        }

        private async Task<T> FetchJsonGetAsync<T>(KunaQuery query) {
            var response = await _client.GetAsync(query.Uri).ConfigureAwait(false);
            return await ReadResponseAsync<T>(response).ConfigureAwait(false);
        }

        private Task<T> FetchAuthJsonGetAsync<T>(KunaQuery query, KunaKey key) {
            return FetchAuthJsonAsync<T>(HttpMethod.Get, query, key);
        }

        private async Task<T> FetchAuthJsonAsync<T>(HttpMethod method, KunaQuery query, KunaKey key) {
            var tonce = DateTimeUtils.ToUnixTimestampMillis(DateTime.UtcNow).ToString();
            query = query.AddQuery("access_key", key.PublicKey).AddQuery("tonce", tonce);
            var signature = key.GetSignature(method, query);
            query = query.AddQuery("signature", signature);
            HttpContent content = null;
            if (method == HttpMethod.Post) {
                content = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");
            }
            var request = new HttpRequestMessage(method, query.Uri) {
                Content = content
            };
            using (var response = await _client.SendAsync(request).ConfigureAwait(false)) {
                return await ReadResponseAsync<T>(response).ConfigureAwait(false);
            }
        }

        private async Task<T> ReadResponseAsync<T>(HttpResponseMessage response) {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<T>(content);
            } else {
                var errorMsg = JsonConvert.DeserializeObject<KunaErrorMessage>(content);
                throw new KunaClientException(errorMsg.Error);
            }
        }

    }
}
