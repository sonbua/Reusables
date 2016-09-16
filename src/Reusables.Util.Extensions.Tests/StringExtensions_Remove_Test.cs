using System;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class StringExtensions_Remove_Test
    {
        [Theory, AutoData]
        public void GivenNullSource_ThrowsException(char[] removedChars)
        {
            // arrange

            // act
            Action nullSource = () => ((string) null).Remove(removedChars);

            // assert
            Assert.Throws<ArgumentNullException>(nullSource);
        }

        [Theory, AutoData]
        public void GivenNullRemovedChars_ThrowsException(string source)
        {
            // arrange

            // act
            Action nullRemovedChars = () => source.Remove((char[]) null);

            // assert
            Assert.Throws<ArgumentNullException>(nullRemovedChars);
        }

        [Theory]
        [InlineData("some string", new char[] {}, "some string")]
        [InlineData("some string", new[] {'x', 'y'}, "some string")]
        [InlineData("some string", new[] {'s', ' '}, "ometring")]
        public void GivenValidRemovedChars_ReturnsStringWithoutRemovedChars(string source, char[] removedChars, string expected)
        {
            // arrange

            // act
            var actual = source.Remove(removedChars);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [AutoData]
        public void GivenNullSource_ThrowsException_2(char[] removedChars)
        {
            // arrange

            // act
            Action nullSource = () => ((string) null).Remove(removedChars);

            // assert
            Assert.Throws<ArgumentNullException>(nullSource);
        }

        [Theory, AutoData]
        public void GivenNullSource_ThrowsException_3(string[] removedStrings)
        {
            // arrange

            // act
            Action nullSource = () => ((string) null).Remove(removedStrings);

            // assert
            Assert.Throws<ArgumentNullException>(nullSource);
        }

        [Theory, AutoData]
        public void GivenNullRemovedStrings_ThrowsException(string source)
        {
            // arrange

            // act
            Action nullRemovedStrings = () => source.Remove((string[]) null);

            // assert
            Assert.Throws<ArgumentNullException>(nullRemovedStrings);
        }

        [Theory]
        [InlineData("some source string", new string[] {}, "some source string")]
        [InlineData("some source string", new[] {"so", " "}, "meurcestring")]
        [InlineData("some source string", new[] {"xx"}, "some source string")]
        public void GivenValidRemovedStrings_ReturnsStringWithoutRemovedStrings(string source, string[] removedStrings, string expected)
        {
            // arrange

            // act
            var actual = source.Remove(removedStrings);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}