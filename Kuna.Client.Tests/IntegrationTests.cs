using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Kuna.Client.Tests {
    [TestFixture/*, Ignore("integration tests")*/]
    public partial class IntegrationTests {

        // [Test]
        // public async Task GetActiveOrdersTest() {
        //     var client = CreateClient();
        //     var orders = await client.GetActiveOrderAsync("btcuah");
        // }

        // [Test]
        // public async Task PlaceOrderTest() {
        //     var client = CreateClient();
        //     var order1 = await client.UseMarket("btcuah").PlaceOrderAsync("buy", 0.0001m, 250000m);
        //     await client.CancelOrderAsync(order1.Id);

        //     var order2 = await client.UseMarket("btcuah").PlaceOrderAsync("sell", 0.0001m, 350000m);
        //     await client.CancelOrderAsync(order2.Id);
        // }

        [Test]
        public async Task GetMeAsyncTest() {
            var client = CreateClient();
            var userInfo = await client.GetMeAsync();
            Assert.IsNotNull(userInfo);
        }

        [Test]
        public async Task GetMyWalletsAsyncTest() {
            var client = CreateClient();
            var userWallets = await client.GetMyWalletsAsync();
            Assert.IsNotNull(userWallets);
        }

        [Test]
        public async Task GetTimestampAsyncTest() {
            var client = CreateClient();
            var serverDate = await client.GetTimestampAsync();
            Assert.LessOrEqual(Math.Abs((serverDate - DateTime.UtcNow).TotalSeconds), 15);
        }

        private KunaAuthClient CreateClient() {
            return new KunaClient().UseKey(CreateKey());
        }

    }
}