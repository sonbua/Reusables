using System;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class StringExtensions_EqualsIgnoreCase_Test
    {
        [Theory, AutoData]
        public void GivenNullSubject_ThrowsException(string other)
        {
            // arrange

            // act
            Action nullSubject = () => ((string) null).EqualsIgnoreCase(other);

            // assert
            Assert.Throws<ArgumentNullException>(nullSubject);
        }

        [Theory]
        [InlineData("")]
        [InlineData("subject")]
        public void GivenNullOther_AlwaysReturnsFalse(string subject)
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
        public void GivenValidInput_ReturnsCorrectResult(string subject, string other, bool expected)
        {
            // arrange

            // act
            var actual = subject.EqualsIgnoreCase(other);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}