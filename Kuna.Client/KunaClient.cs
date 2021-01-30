using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Kuna.Client.Serialization;
using Kuna.Client.Utils;

namespace Kuna.Client {
    public class KunaClient {

        private static readonly HttpClient _client = new HttpClient();
        private static readonly Uri _origin = new Uri("https://api.kuna.io/v3");
        private static MediaTypeWithQualityHeaderValue _applicationJson = new MediaTypeWithQualityHeaderValue("application/json");

        public KunaAuthClient UseKey(KunaKey key) {
            return new KunaAuthClient(this, key);
        }

        public KunaMarketClient UseMarket(string market) {
            return new KunaMarketClient(this, market);
        }

        public async Task<DateTime> GetTimestampAsync() {
            var response = await GetJsonAsync<ServerTime>("/timestamp");
            return DateTimeUtils.FromUnixTimestamp(response.Timestamp);
        }

        // public Task<MarketData> GetMarketDataAsync(string market) {
        //     return FetchJsonGetAsync<MarketData>(new KunaQuery($"https://kuna.io/api/v2/tickers/{market}"));
        // }

        // public Task<OrderBook> GetOrderBookAsync(string market) {
        //     return FetchJsonGetAsync<OrderBook>(new KunaQuery($"https://kuna.io/api/v2/depth?market={market}"));
        // }

        // public Task<IList<Transaction>> GetTradeHistoryAsync(string market) {
        //     var query = new KunaQuery("https://kuna.io/api/v2/trades")
        //         .AddQuery("market", market);
        //     return FetchJsonGetAsync<IList<Transaction>>(query);
        // }

        public Task<UserInfo> GetMeAsync(KunaKey key) {
            return PostAuthJsonAsync<UserInfo>("/auth/me", key);
        }

        public async Task<UserWallets> GetMyWalletsAsync(KunaKey key) {
            var response = await PostAuthJsonAsync<UserWallet[]>("/auth/r/wallets", key);
            return new UserWallets(response);
        }

        // public Task<IList<Transaction>> GetMyTradesAsync(string market, KunaKey key) {
        //     var query = new KunaQuery("https://kuna.io/api/v2/trades/my")
        //         .AddQuery("market", market);
        //     return FetchAuthJsonGetAsync<IList<Transaction>>(query, key);
        // }

        // public Task<Order> PlaceOrderAsync(string side, decimal volume, string market, decimal price, KunaKey key) {
        //     var query = new KunaQuery("https://kuna.io/api/v2/orders")
        //         .AddQuery("side", side)
        //         .AddQuery("volume", volume)
        //         .AddQuery("market", market)
        //         .AddQuery("price", price);
        //     return FetchAuthJsonAsync<Order>(HttpMethod.Post, query, key);
        // }

        // public Task<Order> PlaceBuyOrderAsync(decimal volume, string market, decimal price, KunaKey key) {
        //     var query = new KunaQuery("https://kuna.io/api/v2/orders")
        //         .AddQuery("side", "buy")
        //         .AddQuery("volume", volume)
        //         .AddQuery("market", market)
        //         .AddQuery("price", price);
        //     return FetchAuthJsonAsync<Order>(HttpMethod.Post, query, key);
        // }

        // public Task<Order> PlaceSellOrderAsync(decimal volume, string market, decimal price, KunaKey key) {
        //     var query = new KunaQuery("https://kuna.io/api/v2/orders")
        //         .AddQuery("side", "sell")
        //         .AddQuery("volume", volume)
        //         .AddQuery("market", market)
        //         .AddQuery("price", price);
        //     return FetchAuthJsonAsync<Order>(HttpMethod.Post, query, key);
        // }

        // public Task<Order> CancelOrderAsync(string orderId, KunaKey key) {
        //     var query = new KunaQuery("https://kuna.io/api/v2/order/delete")
        //         .AddQuery("id", orderId);
        //     return FetchAuthJsonAsync<Order>(HttpMethod.Post, query, key);
        // }

        // public Task<IList<Order>> GetActiveOrderAsync(string market, KunaKey key) {
        //     var query = new KunaQuery("https://kuna.io/api/v2/orders")
        //         .AddQuery("market", market);
        //     return FetchAuthJsonGetAsync<IList<Order>>(query, key);
        // }

        // private async Task<T> FetchJsonGetAsync<T>(KunaQuery query) {
        //     var response = await _client.GetAsync(query.Uri).ConfigureAwait(false);
        //     return await ReadResponseAsync<T>(response).ConfigureAwait(false);
        // }

        private Task<TResponse> PostAuthJsonAsync<TRequest, TResponse>(string apiPath, TRequest request, KunaKey key) {
            var json = KunaJson.Serialize(request);
            return PostAuthJsonAsync<TResponse>(apiPath, json, key);
        }

        private Task<TResponse> PostAuthJsonAsync<TResponse>(string apiPath, KunaKey key) {
            return PostAuthJsonAsync<TResponse>(apiPath, "{}", key);
        }

        private async Task<TResponse> PostAuthJsonAsync<TResponse>(string apiPath, string json, KunaKey key) {
            var uri = new Uri($"{_origin}{apiPath}");
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) {
                Content = content
            };
            return await SendAuthAsync<TResponse>(requestMessage, key);
        }

        private async Task<TResponse> GetAuthJsonAsync<TResponse>(string apiPath, KunaKey key) {
            var uri = new Uri($"{_origin}{apiPath}");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendAuthAsync<TResponse>(requestMessage, key);
        }

        private async Task<TResponse> GetJsonAsync<TResponse>(string apiPath) {
            var uri = new Uri($"{_origin}{apiPath}");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendAsync<TResponse>(requestMessage);
        }

        private async Task<TResponse> SendAuthAsync<TResponse>(HttpRequestMessage requestMessage, KunaKey key) {
            var nonce = DateTimeUtils.ToUnixTimestampMillis(DateTime.UtcNow).ToString();
            requestMessage.Headers.Add("kun-nonce", nonce);
            requestMessage.Headers.Add("kun-apikey", key.PublicKey);

            var signature = key.GetSignature(requestMessage);
            requestMessage.Headers.Add("kun-signature", signature);
            return await SendAsync<TResponse>(requestMessage).ConfigureAwait(false);
        }

        private async Task<TResponse> SendAsync<TResponse>(HttpRequestMessage requestMessage) {
            requestMessage.Headers.Accept.Clear();
            requestMessage.Headers.Accept.Add(_applicationJson);

            using var response = await _client.SendAsync(requestMessage).ConfigureAwait(false);
            return await ReadResponseAsync<TResponse>(response).ConfigureAwait(false);
        }

        private async Task<T> ReadResponseAsync<T>(HttpResponseMessage response) {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode) {
                return KunaJson.Deserialize<T>(content);
            } else {
                var errorMsg = KunaJson.Deserialize<KunaErrorMessage>(content);
                throw new KunaClientException(errorMsg.Error);
            }
        }

    }
}
