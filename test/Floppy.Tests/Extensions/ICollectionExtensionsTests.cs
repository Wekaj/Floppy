using Floppy.Extensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Floppy.Tests.Extensions {
    public class ICollectionExtensionsTests {
        [Fact]
        public void AddRange_ArrayToList_AddArrayItemsInOrder() {
            var list = new List<int> { 1, 2, 3 };
            var array = new[] { 4, 5, 6 };

            ICollectionExtensions.AddRange(list, array);

            Assert.Equal(new[] { 1, 2, 3, 4, 5, 6 }, list);
        }

        [Fact]
        public void AddRange_EmptyArrayToList_DoNothing() {
            var list = new List<int> { 1, 2, 3 };
            var array = Array.Empty<int>();

            ICollectionExtensions.AddRange(list, array);

            Assert.Equal(new[] { 1, 2, 3 }, list);
        }
    }
}
