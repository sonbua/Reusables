using System.Collections.Generic;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class EnumerableExtensions_IsNullOrEmpty_Test
    {
        [Fact]
        public void GivenNullEnumerable_ReturnsTrue()
        {
            // arrange
            IEnumerable<object> nullEnumerable = null;

            // act

            // assert
            Assert.True(nullEnumerable.IsNullOrEmpty());
        }

        [Fact]
        public void GivenEmptyEnumerable_ReturnsTrue()
        {
            // arrange
            IEnumerable<object> emptyEnumerable = new List<object>();

            // act

            // assert
            Assert.True(emptyEnumerable.IsNullOrEmpty());
        }

        [Fact]
        public void GivenOneItemEnumerable_ReturnsFalse()
        {
            // arrange
            IEnumerable<object> oneItemEnumerable = new List<object> {new object()};

            // act

            // assert
            Assert.False(oneItemEnumerable.IsNullOrEmpty());
        }

        [Theory, AutoData]
        public void GivenThreeItemsCollection_ReturnsFalse(IEnumerable<object> threeItemsEnumerable)
        {
            // arrange

            // act

            // assert
            Assert.False(threeItemsEnumerable.IsNullOrEmpty());
        }
    }
}