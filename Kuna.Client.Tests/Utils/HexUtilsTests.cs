using System;
using Kuna.Client.Utils;
using NUnit.Framework;

namespace Kuna.Client.Tests {
    [TestFixture, TestOf(typeof(HexUtils))]
    public class HexUtilsTest {

        [Test]
        public void ToHexStringTest() {
            var result = HexUtils.ToHexString(new byte[] { 165, 0, 85 });
            Assert.AreEqual("a50055", result);
        }

        [Test]
        public void ToHexStringNullTest() {
            var result = HexUtils.ToHexString(null);
            Assert.IsNull(result);
        }

        [Test]
        public void FromHexStringLowerTest() {
            var result = HexUtils.FromHexString("a50055");
            Assert.AreEqual(new byte[] { 165, 0, 85 }, result);
        }

        [Test]
        public void FromHexStringUpperTest() {
            var result = HexUtils.FromHexString("A50055");
            Assert.AreEqual(new byte[] { 165, 0, 85 }, result);
        }

        [Test]
        public void FromHexStringHalfTest() {
            var result = HexUtils.FromHexString("a50055a");
            Assert.AreEqual(new byte[] { 165, 0, 85, 160 }, result);
        }

        [Test]
        public void FromHexStringInvalidTest() {
            Assert.Throws<ArgumentException>(() => HexUtils.FromHexString("axyzfg"));
        }

        [Test]
        public void FromHexStringNullTest() {
            var result = HexUtils.FromHexString(null);
            Assert.IsNull(result);
        }

    }
}