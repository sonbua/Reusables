using System;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class ConversionExtensions_GetString_Test
    {
        [Fact]
        public void GivenNullByteArray_ThrowsException()
        {
            // arrange
            byte[] bytes = null;

            // act

            // assert
            Assert.Throws<ArgumentNullException>(() => bytes.GetString());
        }
    }
}