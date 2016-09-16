using System;
using System.Collections.Generic;
using NSubstitute;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class EnumerableExtensions_Join_Test
    {
        private static IEnumerable<object> Objects => new List<object>
                                                      {
                                                          new HelperObject {Prop1 = "1", Prop2 = "2"},
                                                          new HelperObject {Prop1 = "3", Prop2 = "4"}
                                                      };

        private class HelperObject
        {
            public string Prop1 { get; set; }

            public string Prop2 { get; set; }

            public override string ToString()
            {
                return $"{Prop1}:{Prop2}";
            }
        }

        [Fact]
        public void GivenNullSource_ThrowsException()
        {
            // arrange

            // act
            Action nullSource = () => ((IEnumerable<object>) null).Join(Arg.Any<Func<object, string>>(), "any");

            // assert
            Assert.Throws<ArgumentNullException>(nullSource);
        }

        [Fact]
        public void GivenNullSelector_ThrowsException()
        {
            // arrange

            // act
            Action nullSelector = () => Objects.Join(null, "any");

            // assert
            Assert.Throws<ArgumentNullException>(nullSelector);
        }

        [Fact]
        public void GivenNullSeparator_TreatsAsEmptySeparator()
        {
            // arrange

            // act
            var actual = Objects.Join(o => o.ToString(), null);

            // assert
            Assert.Equal("1:23:4", actual);
        }

        [Fact]
        public void GivenValidInput_ReturnsCorrectJoinedString()
        {
            // arrange

            // act
            var actual = Objects.Join(o => o.ToString(), ",");

            // assert
            Assert.Equal("1:2,3:4", actual);
        }
    }
}