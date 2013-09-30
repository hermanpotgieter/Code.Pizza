using System.Collections.Generic;
using System.Linq;
using Code.Pizza.Common.Paging;
using NUnit.Framework;

namespace Code.Pizza.Common.Tests.Paging
{
    [TestFixture]
    public class PagedSetExtensionTests
    {
        [Test]
        [TestCase(1, 1, 1, 1)]
        [TestCase(1, 1, 2, 2)]
        [TestCase(2, 1, 2, 2)]
        [TestCase(1, 10, 25, 3)]
        [TestCase(2, 10, 25, 3)]
        [TestCase(3, 10, 25, 3)]
        [TestCase(1, 10, 95, 10)]
        [TestCase(3, 10, 95, 10)]
        [TestCase(9, 10, 95, 10)]
        [TestCase(10, 10, 95, 10)]
        public void ToPagedSet(int page, int pageSize, int itemCount, int pageCount)
        {
            List<int> source = new List<int>();

            for (int i = 1; i <= itemCount; i++)
            {
                source.Add(i);
            }

            IPagedCollection<int> actual = source.ToPagedSet(page, pageSize);
            int pageStart = ((page - 1) * pageSize) + 1;
            int expectedCount = actual.IsLastPage ? itemCount - ((page - 1) * pageSize) : pageSize;

            Assert.That(page == actual.Page);
            Assert.That(pageSize == actual.PageSize);
            Assert.That(itemCount == actual.ItemCount);
            Assert.That(pageCount == actual.PageCount);

            Assert.That(expectedCount == actual.Count);

            for (int i = 0; i < actual.Count; i++)
            {
                int expected = pageStart++;
                Assert.That(expected == actual.ElementAt(i));
            }
        }
    }
}
