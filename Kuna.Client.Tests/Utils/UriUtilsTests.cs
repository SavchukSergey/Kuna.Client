using System;
using Kuna.Client.Utils;
using NUnit.Framework;

namespace Kuna.Client.Tests {
    [TestFixture, TestOf(typeof(UriUtils))]
    public class UriUtilsTest {

        [TestCase("http://google.com/path", "abc", "def", "http://google.com/path?abc=def")]
        [TestCase("http://google.com/path?xyz=qwe", "abc", "def", "http://google.com/path?xyz=qwe&abc=def")]
        public void AppendQueryTest(string source, string key, string value, string target) {
            var uri = new Uri(source);
            var actual = uri.AppendQuery(key, value);
            var expected = new Uri(target);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("http://google.com/path", "abc", "def", "http://google.com/path?abc=def")]
        [TestCase("http://google.com/path?xyz=qwe", "abc", "def", "http://google.com/path?abc=def&xyz=qwe")]
        public void PrependQueryTest(string source, string key, string value, string target) {
            var uri = new Uri(source);
            var actual = uri.PrependQuery(key, value);
            var expected = new Uri(target);
            Assert.AreEqual(expected, actual);
        }

    }
}