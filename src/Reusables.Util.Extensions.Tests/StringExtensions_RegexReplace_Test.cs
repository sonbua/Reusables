using System;
using System.Text.RegularExpressions;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class StringExtensions_RegexReplace_Test
    {
        [Theory, AutoData]
        public void GivenNullString_ThrowsException(string pattern, string replacement)
        {
            // arrange
            string s = null;

            // act

            // assert
            Assert.Throws<ArgumentNullException>(() => s.RegexReplace(pattern, replacement));
            Assert.Throws<ArgumentNullException>(() => s.RegexReplace(pattern, replacement, RegexOptions.Compiled));
        }

        [Theory, AutoData]
        public void GivenRemoveAllLetters_ReturnsStringContainsNoLetter(string source)
        {
            // arrange
            var letters = @"[a-zA-Z]";

            // act
            var actual = source.RegexReplace(letters, string.Empty);

            // assert
            Assert.False(Regex.IsMatch(actual, letters));
        }
    }
}