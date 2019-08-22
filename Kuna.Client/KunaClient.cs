using System;
using System.Collections.Generic;
using System.Net.Http;
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
            var response = await _client.GetAsync("https://kuna.io/api/v2/timestamp").ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var timestamp = long.Parse(content);
            return DateTimeUtils.FromUnixTimestamp(timestamp);
        }

        public Task<MarketData> GetMarketDataAsync(string market) {
            return FetchJsonGetAsync<MarketData>($"https://kuna.io/api/v2/tickers/{market}");
        }

        public Task<OrderBook> GetOrderBookAsync(string market) {
            return FetchJsonGetAsync<OrderBook>($"https://kuna.io/api/v2/depth?market={market}");
        }

        public Task<IList<Transaction>> GetTradeHistoryAsync(string market) {
            return FetchJsonGetAsync<IList<Transaction>>($"https://kuna.io/api/v2/trades?market={market}");
        }

        public Task<UserInfo> GetMeAsync(KunaKey key) {
            return FetchAuthJsonGetAsync<UserInfo>("https://kuna.io/api/v2/members/me", key);
        }

        public Task<IList<Transaction>> GetMyTradesAsync(string market, KunaKey key) {
            return FetchAuthJsonGetAsync<IList<Transaction>>($"https://kuna.io/api/v2/trades/my?market={market}", key);
        }

        public Task<Order> PlaceOrderAsync(string side, decimal volume, string market, decimal price, KunaKey key) {
            var uri = new Uri("https://kuna.io/api/v2/orders")
                .AppendQuery("side", side)
                .AppendQuery("volume", volume)
                .AppendQuery("market", market)
                .AppendQuery("price", price);
            return FetchAuthJsonAsync<Order>(HttpMethod.Post, uri, key);
        }

        public Task<Order> CancelOrderAsync(string orderId, KunaKey key) {
            var uri = new Uri("https://kuna.io/api/v2/order/delete")
                .AppendQuery("id", orderId);
            return FetchAuthJsonAsync<Order>(HttpMethod.Post, uri, key);
        }

        public Task<IList<Order>> GetActiveOrderAsync(string market, KunaKey key) {
            var uri = new Uri("https://kuna.io/api/v2/orders")
                .AppendQuery("market", market);
            return FetchAuthJsonGetAsync<IList<Order>>(uri, key);
        }

        private async Task<T> FetchJsonGetAsync<T>(string url) {
            var response = await _client.GetAsync(url).ConfigureAwait(false);
            return await ReadResponseAsync<T>(response).ConfigureAwait(false);
        }

        private Task<T> FetchAuthJsonGetAsync<T>(string url, KunaKey key) {
            return FetchAuthJsonAsync<T>(HttpMethod.Get, url, key);
        }

        private Task<T> FetchAuthJsonGetAsync<T>(Uri uri, KunaKey key) {
            return FetchAuthJsonAsync<T>(HttpMethod.Get, uri, key);
        }

        private Task<T> FetchAuthJsonAsync<T>(HttpMethod method, string url, KunaKey key) {
            return FetchAuthJsonAsync<T>(method, new Uri(url), key);
        }

        private async Task<T> FetchAuthJsonAsync<T>(HttpMethod method, Uri uri, KunaKey key) {
            var tonce = DateTimeUtils.ToUnixTimestampMillis(DateTime.UtcNow).ToString();
            uri = uri.PrependQuery("access_key", key.PublicKey).AppendQuery("tonce", tonce);
            var signature = key.GetSignature(method, uri);
            uri = uri.AppendQuery("signature", signature);
            var request = new HttpRequestMessage(method, uri);
            var response = await _client.SendAsync(request).ConfigureAwait(false);
            return await ReadResponseAsync<T>(response).ConfigureAwait(false);
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
