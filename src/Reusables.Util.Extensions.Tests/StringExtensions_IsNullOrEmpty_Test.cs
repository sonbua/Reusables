using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class StringExtensions_IsNullOrEmpty_Test
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("some string", false)]
        public void GivenSomeString_ReturnsExpectedValue(string input, bool expected)
        {
            // arrange

            // act
            var actual = input.IsNullOrEmpty();

            // assert
            Assert.Equal(expected, actual);
        }
    }
}