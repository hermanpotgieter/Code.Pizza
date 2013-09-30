using System;
using System.Collections.Generic;
using Code.Pizza.Common.Paging;
using NUnit.Framework;

namespace Code.Pizza.Common.Tests.Paging
{
    [TestFixture]
    public class PagedSetTests
    {
        private int page;
        private int pageSize;
        private int itemCount;

        private int pageCount;

        private HashSet<int> source;

        [SetUp]
        public void Setup()
        {
            this.page = 1;
            this.pageSize = 1;
            this.itemCount = 2;

            this.pageCount = this.itemCount > 0 ? (int)Math.Ceiling(this.itemCount / (double)this.pageSize) : 0;

            this.source = new HashSet<int>(new[] { 1, 2 });
        }

        [Test]
        public void Constructor_null_source_throws_ArgumentNullException()
        {
            this.source = null;
            Assert.Throws<ArgumentNullException>(() => new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount));
        }

        [Test]
        public void Constructor_page_less_than_One_throws_ArgumentOutOfRangeException()
        {
            this.page = 0;
            Assert.Throws<ArgumentOutOfRangeException>(() => new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount));
        }

        [Test]
        public void Constructor_page_greater_than_pageCount_throws_ArgumentOutOfRangeException()
        {
            this.page = this.pageCount + 1;
            Assert.Throws<ArgumentOutOfRangeException>(() => new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount));
        }

        [Test]
        public void Constructor_pageSize_less_than_One_throws_ArgumentOutOfRangeException()
        {
            this.pageSize = 0;
            Assert.Throws<ArgumentOutOfRangeException>(() => new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount));
        }

        [Test]
        public void Constructor_itemCount_less_than_Zero_throws_ArgumentOutOfRangeException()
        {
            this.itemCount = -1;
            Assert.Throws<ArgumentOutOfRangeException>(() => new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount));
        }

        [Test]
        public void Constructor_itemCount_less_than_source_Count_throws_ArgumentOutOfRangeException()
        {
            this.itemCount = this.source.Count - 1;
            Assert.Throws<ArgumentOutOfRangeException>(() => new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount));
        }

        [Test]
        public void Constructor_valid_arguments_does_not_throw()
        {
            Assert.DoesNotThrow(() => new PagedSet<int>(this.source, this.page, this.pageCount, this.itemCount));
        }

        [Test]
        public void Constructor_sets_Page()
        {
            PagedSet<int> pagedSet = new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount);

            Assert.That(pagedSet.Page == this.page);
        }

        [Test]
        public void Constructor_sets_PageSize()
        {
            PagedSet<int> pagedSet = new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount);

            Assert.That(pagedSet.PageSize == this.pageSize);
        }

        [Test]
        public void Constructor_sets_ItemCount()
        {
            PagedSet<int> pagedSet = new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount);

            Assert.That(pagedSet.ItemCount == this.itemCount);
        }

        [Test]
        public void Constructor_sets_PageCount()
        {
            PagedSet<int> pagedSet = new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount);

            Assert.That(pagedSet.PageCount == this.pageCount);
        }

        [Test]
        public void IsFirstPage_Page_equals_One_returns_True()
        {
            this.page = 1;
            PagedSet<int> pagedSet = new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount);

            Assert.That(pagedSet.IsFirstPage);
        }

        [Test]
        public void IsFirstPage_Page_greater_than_One_returns_False()
        {
            this.page = 2;
            PagedSet<int> pagedSet = new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount);

            Assert.That(pagedSet.IsFirstPage == false);
        }

        [Test]
        public void IsLastPage_Page_equals_PageCount_returns_True()
        {
            this.page = this.pageCount;
            PagedSet<int> pagedSet = new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount);

            Assert.That(pagedSet.IsLastPage);
        }

        [Test]
        public void IsLastPage_Page_less_than_PageCount_returns_False()
        {
            this.page = this.pageCount - 1;
            PagedSet<int> pagedSet = new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount);

            Assert.That(pagedSet.IsLastPage == false);
        }

        [Test]
        public void HasPreviousPage_Page_equals_One_returns_False()
        {
            this.page = 1;
            PagedSet<int> pagedSet = new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount);

            Assert.That(pagedSet.HasPreviousPage == false);
        }

        [Test]
        public void HasPreviousPage_Page_greater_than_One_returns_True()
        {
            this.page = 2;
            PagedSet<int> pagedSet = new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount);

            Assert.That(pagedSet.HasPreviousPage);

        }

        [Test]
        public void HasNextPage_Page_equals_PageCount_returns_False()
        {
            this.page = this.pageCount;
            PagedSet<int> pagedSet = new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount);

            Assert.That(pagedSet.HasNextPage == false);
        }

        [Test]
        public void HasNextPage_Page_less_than_PageCount_returns_True()
        {
            this.page = this.pageCount - 1;
            PagedSet<int> pagedSet = new PagedSet<int>(this.source, this.page, this.pageSize, this.itemCount);

            Assert.That(pagedSet.HasNextPage);

        }
    }
}
