using System;
using System.Text.RegularExpressions;
using Brick.MiscUtil.Extensions;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Brick.MiscUtil.Tests.Extensions
{
    public class StringExtensionsTest
    {
        [Fact]
        public void EqualsIgnoreCase_NullSubject_ThrowsException()
        {
            // arrange
            string subject = null;

            // act
            Action nullSubject = () => subject.EqualsIgnoreCase("other");

            // assert
            Assert.Throws<ArgumentNullException>(nullSubject);
        }

        [Theory]
        [InlineData("")]
        [InlineData("subject")]
        public void EqualsIgnoreCase_NullOther_AlwaysReturnsFalse(string subject)
        {
            // arrange

            // act
            var actual = subject.EqualsIgnoreCase(null);

            // assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData("", "", true)]
        [InlineData("Subject", "subjecT", true)]
        [InlineData("abc", "abd", false)]
        public void EqualsIgnoreCase_ValidInput_ReturnsCorrectResult(string subject, string other, bool expected)
        {
            // arrange

            // act
            var actual = subject.EqualsIgnoreCase(other);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatWithMask_NullSubject_ThrowsException()
        {
            // arrange
            string source = null;
            var mask = "#";

            // act
            Action nullSubject = () => source.FormatWithMask(mask);

            // assert
            Assert.Throws<ArgumentNullException>(nullSubject);
        }

        [Fact]
        public void FormatWithMask_NullMask_ThrowsException()
        {
            // arrange
            var source = string.Empty;
            string mask = null;

            // act
            Action nullMask = () => source.FormatWithMask(mask);

            // assert
            Assert.Throws<ArgumentNullException>(nullMask);
        }

        [Theory]
        [InlineData("123456789", "(###) ###-###", "(123) 456-789")]
        [InlineData("123456", "###-", "123-")]
        [InlineData("12345", "###-###", "123-45")]
        [InlineData("12345", "###-##-", "123-45-")]
        [InlineData("123", "###-##-", "123--")]
        public void FormatWithMask_ShouldFormatCorrectly(string source, string mask, string maskedInput)
        {
            Assert.Equal(maskedInput, source.FormatWithMask(mask));
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("some string", true)]
        public void IsNullOrEmpty_ShouldReturnExpectedValue(string input, bool expected)
        {
            // arrange

            // act
            var actual = input.IsNullOrEmpty();

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [AutoData]
        public void RegexReplace_NullString_ThrowsException(string pattern, string replacement)
        {
            // arrange
            string s = null;

            // act

            // assert
            Assert.Throws<ArgumentNullException>(() => s.RegexReplace(pattern, replacement));
            Assert.Throws<ArgumentNullException>(() => s.RegexReplace(pattern, replacement, RegexOptions.Compiled));
        }

        [Theory]
        [AutoData]
        public void RegexReplace_RemoveAllLetters_ReturnsStringContainsNoLetter(string source)
        {
            // arrange
            var letters = @"[a-zA-Z]";

            // act
            var actual = source.RegexReplace(letters, string.Empty);

            // assert
            Assert.True(!Regex.IsMatch(actual, letters));
        }

        [Fact]
        public void Remove_NullSource_ThrowsException()
        {
            // arrange
            string source = null;

            // act
            Action nullRemovedChars = () => source.Remove(new[] {' '});

            // assert
            Assert.Throws<ArgumentNullException>(nullRemovedChars);
        }

        [Theory]
        [AutoData]
        public void Remove_NullRemovedChars_ThrowsException(string source)
        {
            // arrange
            char[] removedChars = null;

            // act
            Action nullRemovedChars = () => source.Remove(removedChars);

            // assert
            Assert.Throws<ArgumentNullException>(nullRemovedChars);
        }

        [Theory]
        [InlineData("some string", new char[] {}, "some string")]
        [InlineData("some string", new[] {'x', 'y'}, "some string")]
        [InlineData("some string", new[] {'s', ' '}, "ometring")]
        public void Remove_ValidRemovedChars_ReturnsStringWithoutRemovedChars(string source, char[] removedChars, string expected)
        {
            // arrange

            // act
            var actual = source.Remove(removedChars);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Remove_NullSource_ThrowsException_2()
        {
            // arrange
            string source = null;

            // act
            Action nullSource = () => source.Remove(new[] {'x', 'y'});

            // assert
            Assert.Throws<ArgumentNullException>(nullSource);
        }

        [Theory]
        [AutoData]
        public void Remove_NullSource_ThrowsException_3(string[] removedStrings)
        {
            // arrange
            string source = null;

            // act
            Action nullSource = () => source.Remove(removedStrings);

            // assert
            Assert.Throws<ArgumentNullException>(nullSource);
        }

        [Theory]
        [AutoData]
        public void Remove_NullRemovedStrings_ThrowsException(string source)
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
        public void Remove_ValidRemovedStrings_ReturnsStringWithoutRemovedStrings(string source, string[] removedStrings, string expected)
        {
            // arrange

            // act
            var actual = source.Remove(removedStrings);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Replace_NullSubject_ThrowsException()
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
        public void Replace_NullOldChars_ThrowsException()
        {
            // arrange
            var source = string.Empty;
            char[] oldChars = null;
            var substituent = string.Empty;

            // act
            Action nullOldChars = () => source.Replace(oldChars, substituent);

            // assert
            Assert.Throws<ArgumentNullException>(nullOldChars);
        }

        [Theory]
        [AutoData]
        public void Replace_EmptySource_ReturnsEmptyString(char[] oldChars, string substituent)
        {
            // arrange
            var source = string.Empty;

            // act
            var actual = source.Replace(oldChars, substituent);

            // assert
            Assert.Empty(actual);
        }

        [Theory]
        [AutoData]
        public void Replace_EmptyOldChars_ReturnsSameString(string source, string substituent)
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
        public void Replace_ShouldReplaceCorrectly(string source, char[] oldChars, string substituent, string expected)
        {
            // arrange

            // act
            var newString = source.Replace(oldChars, substituent);

            // assert
            Assert.Equal(expected, newString);
        }

        [Fact]
        public void Replace_NullSource_ThrowsException()
        {
            // arrange
            string source = null;

            // act
            Action nullSource = () => source.Replace(new[] {"1"}, "substitutent");

            // assert
            Assert.Throws<ArgumentNullException>(nullSource);
        }

        [Theory]
        [AutoData]
        public void Replace_NullOldStrings_ThrowsException(string source)
        {
            // arrange

            // act
            Action nullSource = () => source.Replace(oldStrings: null, substituent: "substitutent");

            // assert
            Assert.Throws<ArgumentNullException>(nullSource);
        }

        [Theory]
        [InlineData("source", new[] {"s"}, null, "ource")]
        [InlineData("source", new[] {"s"}, "", "ource")]
        public void Replace_NullOrEmptySubstituent_RemovesOldStringsFromSource(string source, string[] oldStrings, string substituent, string expected)
        {
            // arrange

            // act
            var actual = source.Replace(oldStrings, substituent);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("source", new[] {"o", "u", "e"}, "x", "sxxrcx")]
        public void Replace_ValidInput_ShouldReplaceCorrectly(string source, string[] oldStrings, string substituent, string expected)
        {
            // arrange

            // act
            var actual = source.Replace(oldStrings, substituent);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SafeFormat_NullFormat_ThrowsException()
        {
            // arrange
            string format = null;

            // act
            Action nullFormat = () => format.SafeFormat("args");

            // assert
            Assert.Throws<ArgumentNullException>(nullFormat);
        }

        [Fact]
        public void SafeFormat_ArgsSizeIsLessThanNeeded_ThrowsException()
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
        public void SafeFormat_ValidInput_ReturnsCorrectResult(string format, object[] args, string expected)
        {
            // arrange

            // act
            var actual = format.SafeFormat(args);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
