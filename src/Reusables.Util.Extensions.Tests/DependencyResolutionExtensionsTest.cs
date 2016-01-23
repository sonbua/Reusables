using System;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class DependencyResolutionExtensionsTest
    {
        [Fact]
        public void DecoratedWith_NonCompatibleDecoratorType_ThrowsException()
        {
            // arrange
            var decoratee = new Derived();

            // act
            Action nonCompatibleDecoratorType = () => decoratee.DecoratedWith(typeof(NonCompatible));

            // assert
            Assert.Throws<ArgumentException>(nonCompatibleDecoratorType);
        }

        [Fact]
        public void DecoratedWith_OneDecorator_ReturnsCorrectResult()
        {
            // arrange
            var decoratee = new Derived();

            // act
            var decorated = ((IBase) decoratee).DecoratedWith(typeof(Decorator1));

            // assert
            Assert.Equal("_decorator1_derived", decorated.Do());
        }

        [Fact]
        public void DecoratedWith_TwoDecorators_ReturnsCorrectResult()
        {
            // arrange
            IBase decoratee = new Derived();

            // act
            var decorated = decoratee.DecoratedWith(typeof(Decorator1)).DecoratedWith(typeof(Decorator2));

            // assert
            Assert.Equal("_decorator2_decorator1_derived", decorated.Do());
        }
    }

    internal interface IBase
    {
        string Do();
    }

    internal class Derived : IBase
    {
        public string Do()
        {
            return "_derived";
        }
    }

    internal class Decorator1 : IBase
    {
        private readonly IBase _decoratee;

        public Decorator1(IBase decoratee)
        {
            _decoratee = decoratee;
        }

        public string Do()
        {
            return "_decorator1" + _decoratee.Do();
        }
    }

    internal class Decorator2 : IBase
    {
        private readonly IBase _decoratee;

        public Decorator2(IBase decoratee)
        {
            _decoratee = decoratee;
        }

        public string Do()
        {
            return "_decorator2" + _decoratee.Do();
        }
    }

    internal class NonCompatible
    {
    }
}
