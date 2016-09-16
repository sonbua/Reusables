using System;
using System.Collections.Generic;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class EnumerableExtensions_BuildString_Test
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
            Action nullSource = () => ((IEnumerable<object>) null).BuildString(Arg.Any<Func<object, string>>());

            // assert
            Assert.Throws<ArgumentNullException>(nullSource);
        }

        [Theory, AutoData]
        public void GivenNullSelector_ThrowsException(IEnumerable<object> objects)
        {
            // arrange

            // act
            Action nullSelector = () => objects.BuildString(null);

            // assert
            Assert.Throws<ArgumentNullException>(nullSelector);
        }

        [Fact]
        public void GivenValidInput_ReturnsCorrectConcatenatedString()
        {
            // arrange

            // act
            var buildString = Objects.BuildString(o => o + ",");

            // assert
            Assert.Equal("1:2,3:4,", buildString);
        }
    }
}