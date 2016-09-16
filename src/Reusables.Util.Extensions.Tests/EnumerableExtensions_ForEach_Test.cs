using System;
using System.Collections.Generic;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class EnumerableExtensions_ForEach_Test
    {
        [Fact]
        public void GivenNullEnumerable_ThrowsException()
        {
            // arrange

            // act
            Action nullEnumerable = () => ((IEnumerable<object>) null).ForEach(Arg.Any<Action<object>>());

            // assert
            Assert.Throws<ArgumentNullException>(nullEnumerable);
        }

        [Theory, AutoData]
        public void GivenNullAction_ThrowsException(IEnumerable<object> enumerable)
        {
            // arrange

            // act
            Action nullAction = () => enumerable.ForEach(null);

            // assert
            Assert.Throws<ArgumentNullException>(nullAction);
        }

        [Theory, AutoData]
        public void GivenActionOnValidEnumerable_DoesNotThrowException(IEnumerable<object> enumerable)
        {
            // arrange

            // act
            enumerable.ForEach(item => { });

            // assert
        }
    }
}