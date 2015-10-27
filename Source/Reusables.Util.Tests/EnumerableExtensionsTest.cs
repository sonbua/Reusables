using System;
using System.Collections.Generic;
using Brick.MiscUtil.Extensions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Brick.MiscUtil.Tests.Extensions
{
    public class EnumerableExtensionsTest
    {
        private static IEnumerable<object> Objects
        {
            get
            {
                return new List<object>
                       {
                           new HelperObject {Prop1 = "1", Prop2 = "2"},
                           new HelperObject {Prop1 = "3", Prop2 = "4"},
                       };
            }
        }

        [Fact]
        public void IsNullOrEmpty_NullEnumerable_ReturnsTrue()
        {
            // arrange
            IEnumerable<object> nullEnumerable = null;

            // act

            // assert
            Assert.True(nullEnumerable.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_EmptyEnumerable_ReturnsTrue()
        {
            // arrange
            IEnumerable<object> emptyEnumerable = new List<object>();

            // act

            // assert
            Assert.True(emptyEnumerable.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_OneItemEnumerable_ReturnsFalse()
        {
            // arrange
            IEnumerable<object> oneItemEnumerable = new List<object> {new object()};

            // act

            // assert
            Assert.False(oneItemEnumerable.IsNullOrEmpty());
        }

        [Theory]
        [AutoData]
        public void IsNullOrEmpty_ThreeItemsCollection_ReturnsFalse(IEnumerable<object> threeItemsEnumerable)
        {
            // arrange

            // act

            // assert
            Assert.False(threeItemsEnumerable.IsNullOrEmpty());
        }

        [Fact]
        public void ForEach_NullEnumerable_ThrowsException()
        {
            // arrange

            // act
            Action nullEnumerable = () => ((IEnumerable<object>) null).ForEach(Arg.Any<Action<object>>());

            // assert
            Assert.Throws<ArgumentNullException>(nullEnumerable);
        }

        [Theory]
        [AutoData]
        public void ForEach_NullAction_ThrowsException(IEnumerable<object> enumerable)
        {
            // arrange

            // act
            Action nullAction = () => enumerable.ForEach(null);

            // assert
            Assert.Throws<ArgumentNullException>(nullAction);
        }

        [Theory]
        [AutoData]
        public void ForEach_ActionOnValidEnumerable_DoesNotThrowException(IEnumerable<object> enumerable)
        {
            // arrange

            // act
            enumerable.ForEach(item => { });

            // assert
        }

        [Fact]
        public void BuildString_NullSource_ThrowsException()
        {
            // arrange

            // act
            Action nullSource = () => ((IEnumerable<object>) null).BuildString(Arg.Any<Func<object, string>>());

            // assert
            Assert.Throws<ArgumentNullException>(nullSource);
        }

        [Theory]
        [AutoData]
        public void BuildString_NullSelector_ThrowsException(IEnumerable<object> objects)
        {
            // arrange

            // act
            Action nullSelector = () => objects.BuildString(null);

            // assert
            Assert.Throws<ArgumentNullException>(nullSelector);
        }

        [Fact]
        public void BuildString_ValidInput_ReturnsCorrectConcatenatedString()
        {
            // arrange

            // act
            var buildString = Objects.BuildString(o => o + ",");

            // assert
            Assert.Equal("1:2,3:4,", buildString);
        }

        [Fact]
        public void Join_NullSource_ThrowsException()
        {
            // arrange

            // act
            Action nullSource = () => ((IEnumerable<object>) null).Join(Arg.Any<Func<object, string>>(), "any");

            // assert
            Assert.Throws<ArgumentNullException>(nullSource);
        }

        [Fact]
        public void Join_NullSelector_ThrowsException()
        {
            // arrange

            // act
            Action nullSelector = () => Objects.Join(null, "any");

            // assert
            Assert.Throws<ArgumentNullException>(nullSelector);
        }

        [Fact]
        public void Join_NullSeparator_TreatsAsEmptySeparator()
        {
            // arrange

            // act
            var actual = Objects.Join(o => o.ToString(), null);

            // assert
            Assert.Equal("1:23:4", actual);
        }

        [Fact]
        public void Join_ValidInput_ReturnsCorrectJoinedString()
        {
            // arrange

            // act
            var actual = Objects.Join(o => o.ToString(), ",");

            // assert
            Assert.Equal("1:2,3:4", actual);
        }

        [Fact]
        public void IsIn_NullObject_ReturnsFalse()
        {
            // arrange

            // act
            var isInWithNullObject = ((object) null).IsIn(new List<object>());

            // assert
            Assert.False(isInWithNullObject);
        }

        [Fact]
        public void IsIn_NullCollection_ThrowsException()
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
        public void IsIn_ValidInput_ReturnsCorrectResult(int item, IEnumerable<int> collection, bool expected)
        {
            // arrange

            // act
            var actual = item.IsIn(collection);

            // assert
            Assert.Equal(expected, actual);
        }

        private class HelperObject
        {
            public string Prop1 { get; set; }

            public string Prop2 { get; set; }

            public override string ToString()
            {
                return string.Format("{0}:{1}", Prop1, Prop2);
            }
        }
    }
}
