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

        private KunaAuthClient CreateClient() {
            return new KunaClient().UseKey(CreateKey());
        }

    }
}