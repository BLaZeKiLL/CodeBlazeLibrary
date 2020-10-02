using CodeBlaze.Library.Collections.Pools;

using NUnit.Framework;

namespace CodeBlaze.Library.Tests.Editor.Collections.Pools {

    public class LazyObjectPoolTests {

        [Test]
        public void ClaimShouldReturnObjectsInFiFoOrder() {
            var pool = new LazyObjectPool<string>(5, index => $"Hello {index}");

            for (int i = 0; i < pool.Size; i++) {
                var obj = pool.Claim();
                Assert.That(obj, Is.EqualTo($"Hello {i}"));
            }
        }

    }

}