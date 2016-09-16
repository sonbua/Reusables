using System;
using System.Collections.Generic;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class EnumerableExtensions_IsIn_Test
    {
        [Fact]
        public void GivenNullObject_ReturnsFalse()
        {
            // arrange

            // act
            var isInWithNullObject = ((object) null).IsIn(new List<object>());

            // assert
            Assert.False(isInWithNullObject);
        }

        [Fact]
        public void GivenNullCollection_ThrowsException()
        {
            // arrange

            // act
            Action nullCollection = () => new object().IsIn(null);

            // assert
            Assert.Throws<ArgumentNullException>(nullCollection);
        }

        [Theory]
        [InlineData(1, new[] {1, 2, 3}, true)]
        [InlineData(0, new[] {1, 2, 3}, false)]
        public void GivenValidInput_ReturnsCorrectResult(int item, IEnumerable<int> collection, bool expected)
        {
            // arrange

            // act
            var actual = item.IsIn(collection);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}