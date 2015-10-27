using System;
using Brick.MiscUtil.Extensions;
using Xunit;

namespace Brick.MiscUtil.Tests.Extensions
{
    public class ConversionExtensionsTest
    {
        [Fact]
        public void GetBytes_NullString_ThrowsException()
        {
            // arrange
            string str = null;

            // act

            // assert
            Assert.Throws<ArgumentNullException>(() => str.GetBytes());
        }

        [Fact]
        public void GetString_NullByteArray_ThrowsException()
        {
            // arrange
            byte[] bytes = null;

            // act

            // assert
            Assert.Throws<ArgumentNullException>(() => bytes.GetString());
        }
    }
}