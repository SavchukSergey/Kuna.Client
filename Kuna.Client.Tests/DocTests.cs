using System;
using System.Threading.Tasks;

namespace Kuna.Client.Tests {
    public class DocTests {

        public async Task GetMyTradesTest() {
            var client = new KunaClient().UseKey(new KunaKey("pub", "pri")).UseMarket("btcuah");
            var trades = await client.GetMyTradesAsync();
            Console.WriteLine(trades);
        }

        public async Task PlaceBuyOrderTest() {
            var client = new KunaClient().UseKey(new KunaKey("pub", "pri")).UseMarket("btcuah");
            var order = await client.PlaceBuyOrderAsync(1m, 260000m);
            Console.WriteLine(order);
        }

        public async Task PlaceSellOrderTest() {
            var client = new KunaClient().UseKey(new KunaKey("pub", "pri")).UseMarket("btcuah");
            var order = await client.PlaceSellOrderAsync(1m, 260000m);
            Console.WriteLine(order);
        }

        public async Task GetMarketDataTest() {
            var client = new KunaClient().UseKey(new KunaKey("pub", "pri")).UseMarket("btcuah");
            var marketData = await client.GetMarketDataAsync();
            Console.WriteLine(marketData);
        }

    }
}