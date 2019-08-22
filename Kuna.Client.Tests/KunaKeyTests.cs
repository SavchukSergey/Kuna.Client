using System;
using System.Net.Http;
using NUnit.Framework;

namespace Kuna.Client.Tests {
    [TestFixture]
    public class KunaKeyTests {

        [Test]
        public void CtorTest() {
            Assert.DoesNotThrow(() => new KunaKey("pub", "pri"));
            Assert.Throws<ArgumentNullException>(() => new KunaKey(null, "pri"));
            Assert.Throws<ArgumentNullException>(() => new KunaKey("pub", null));
        }

        [Test]
        public void GetSignatureTest() {
            var key = new KunaKey("dV6vEJe1CO", "AYifzxC3Xo");
            var actual = key.GetSignature(HttpMethod.Get, new Uri("https://kuna.io/api/v2/trades/my?access_key=dV6vEJe1CO&market=btcuah&tonce=1465850766246"));
            var expected = "33a694498a2a70cb4ca9a7e28224321e20b41f10217604e9de80ff4ee8cf310e";
            Assert.AreEqual(expected, actual);
        }
    }
}