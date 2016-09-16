using System;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class ConversionExtensions_GetBytes_Test
    {
        [Fact]
        public void GivenNullString_ThrowsException()
        {
            // arrange
            string str = null;

            // act

            // assert
            Assert.Throws<ArgumentNullException>(() => str.GetBytes());
        }
    }
}