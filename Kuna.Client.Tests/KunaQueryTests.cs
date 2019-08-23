using System;
using NUnit.Framework;

namespace Kuna.Client.Tests {
    [TestFixture, TestOf(typeof(KunaQuery))]
    public class KunaQueryTest {

        [TestCase("http://google.com/path", "http://google.com/path?abc=def", "abc", "def")]
        [TestCase("http://google.com/path", "http://google.com/path?abc=def&xyz=qwe", "abc", "def", "xyz", "qwe")]
        [TestCase("http://google.com/path", "http://google.com/path?abc=def&xyz=qwe", "xyz", "qwe", "abc", "def")]
        public void AddQueryTest(string source, string target, params string[] keyValues) {
            var query = new KunaQuery(source);
            for (var i = 0; i < keyValues.Length; i += 2) {
                query = query.AddQuery(keyValues[i], keyValues[i + 1]);
            }
            var actual = query.Uri;
            var expected = new Uri(target);
            Assert.AreEqual(expected, actual);
        }

    }
}