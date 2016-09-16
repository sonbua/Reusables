using System;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class StringExtensions_FormatWithMask_Test
    {
        [Theory, AutoData]
        public void GivenNullSubject_ThrowsException(string mask)
        {
            // arrange

            // act
            Action nullSubject = () => ((string) null).FormatWithMask(mask);

            // assert
            Assert.Throws<ArgumentNullException>(nullSubject);
        }

        [Theory, AutoData]
        public void GivenNullMask_ThrowsException(string source)
        {
            // arrange

            // act
            Action nullMask = () => source.FormatWithMask(null);

            // assert
            Assert.Throws<ArgumentNullException>(nullMask);
        }

        [Theory]
        [InlineData("123456789", "(###) ###-###", "(123) 456-789")]
        [InlineData("123456", "###-", "123-")]
        [InlineData("12345", "###-###", "123-45")]
        [InlineData("12345", "###-##-", "123-45-")]
        [InlineData("123", "###-##-", "123--")]
        public void GivenSourceWithMask_ShouldFormatCorrectly(string source, string mask, string maskedInput)
        {
            Assert.Equal(maskedInput, source.FormatWithMask(mask));
        }
    }
}