using System.Threading.Tasks;
using NUnit.Framework;

namespace Kuna.Client.Tests {
    [TestFixture, Ignore("integration tests")]
    public partial class IntegrationTests {

        [Test]
        public async Task GetActiveOrdersTest() {
            var client = CreateClient();
            var orders = await client.GetActiveOrderAsync("btcuah");
        }

        [Test]
        public async Task PlaceOrderTest() {
            var client = CreateClient();
            var order1 = await client.UseMarket("btcuah").PlaceOrderAsync("buy", 0.0001m, 250000m);
            await client.CancelOrderAsync(order1.Id);

            var order2 = await client.UseMarket("btcuah").PlaceOrderAsync("sell", 0.0001m, 350000m);
            await client.CancelOrderAsync(order2.Id);
        }

        private KunaAuthClient CreateClient() {
            return new KunaClient().UseKey(CreateKey());
        }

    }
}