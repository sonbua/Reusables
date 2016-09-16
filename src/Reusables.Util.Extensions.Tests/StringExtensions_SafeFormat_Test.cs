using System;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class StringExtensions_SafeFormat_Test
    {
        [Theory, AutoData]
        public void GivenNullFormat_ThrowsException(string args)
        {
            // arrange

            // act
            Action nullFormat = () => ((string) null).SafeFormat(args);

            // assert
            Assert.Throws<ArgumentNullException>(nullFormat);
        }

        [Fact]
        public void GivenArgsSizeIsLessThanNeeded_ThrowsException()
        {
            // arrange
            var format = ".{0},{1}.";

            // act
            Action argsSizeIsLessThanNeeded = () => format.SafeFormat("arg1");

            // assert
            Assert.Throws<FormatException>(argsSizeIsLessThanNeeded);
        }

        [Theory]
        [InlineData(".{0}.", null, "..")]
        [InlineData("..", null, "..")]
        [InlineData("..", new object[] {}, "..")]
        [InlineData(".{0},{1}.", new object[] {null, null}, ".,.")]
        [InlineData(".{0},{1}.", new object[] {null, "arg1"}, ".,arg1.")]
        public void GivenValidInput_ReturnsCorrectResult(string format, object[] args, string expected)
        {
            // arrange

            // act
            var actual = format.SafeFormat(args);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}