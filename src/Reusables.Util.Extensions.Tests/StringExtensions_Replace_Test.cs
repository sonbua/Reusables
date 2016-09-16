using System;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class StringExtensions_Replace_Test
    {
        [Fact]
        public void GivenNullSubject_ThrowsException()
        {
            // arrange
            char[] oldChars = {' '};
            var substituent = string.Empty;

            // act
            Action nullSubject = () => ((string) null).Replace(oldChars, substituent);

            // assert
            Assert.Throws<ArgumentNullException>(nullSubject);
        }

        [Fact]
        public void GivenNullOldChars_ThrowsException()
        {
            // arrange
            var source = string.Empty;
            var substituent = string.Empty;

            // act
            Action nullOldChars = () => source.Replace((char[]) null, substituent);

            // assert
            Assert.Throws<ArgumentNullException>(nullOldChars);
        }

        [Theory, AutoData]
        public void GivenEmptySource_ReturnsEmptyString(char[] oldChars, string substituent)
        {
            // arrange
            var source = string.Empty;

            // act
            var actual = source.Replace(oldChars, substituent);

            // assert
            Assert.Empty(actual);
        }

        [Theory, AutoData]
        public void GivenEmptyOldChars_ReturnsSameString(string source, string substituent)
        {
            // arrange
            var emptyOldChars = new char[] {};

            // act
            var actual = source.Replace(emptyOldChars, substituent);

            // assert
            Assert.Equal(source, actual);
        }

        [Theory]
        [InlineData("test", new[] {'s', 't'}, null, "e")]
        [InlineData("(111) 222-333", new[] {'(', ')', ' ', '-'}, "", "111222333")]
        public void GivenShouldReplaceCorrectly(string source, char[] oldChars, string substituent, string expected)
        {
            // arrange

            // act
            var newString = source.Replace(oldChars, substituent);

            // assert
            Assert.Equal(expected, newString);
        }

        [Theory, AutoData]
        public void GivenNullSource_ThrowsException(string[] oldStrings, string substitutent)
        {
            // arrange

            // act
            Action nullSource = () => ((string) null).Replace(oldStrings, substitutent);

            // assert
            Assert.Throws<ArgumentNullException>(nullSource);
        }

        [Theory, AutoData]
        public void GivenNullOldStrings_ThrowsException(string source, string substitutent)
        {
            // arrange

            // act
            Action nullSource = () => source.Replace((string[]) null, substitutent);

            // assert
            Assert.Throws<ArgumentNullException>(nullSource);
        }

        [Theory]
        [InlineData("source", new[] {"s"}, null, "ource")]
        [InlineData("source", new[] {"s"}, "", "ource")]
        public void GivenNullOrEmptySubstituent_RemovesOldStringsFromSource(string source, string[] oldStrings, string substituent, string expected)
        {
            // arrange

            // act
            var actual = source.Replace(oldStrings, substituent);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("source", new[] {"o", "u", "e"}, "x", "sxxrcx")]
        public void GivenValidInput_ShouldReplaceCorrectly(string source, string[] oldStrings, string substituent, string expected)
        {
            // arrange

            // act
            var actual = source.Replace(oldStrings, substituent);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}