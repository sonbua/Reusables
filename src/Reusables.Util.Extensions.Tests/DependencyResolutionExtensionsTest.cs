using System;
using SimpleInjector;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class DependencyResolutionExtensionsTest
    {
        private readonly Container _container;

        public DependencyResolutionExtensionsTest()
        {
            _container = new Container();

            _container.Register<IDependency, Dependency>();

            _container.Verify();
        }

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

        [Fact]
        public void DecoratedWith_DecoratorWithDependency_ReturnsCorrectResult()
        {
            // arrange
            IBase decoratee = new Derived();
            DefaultServiceProvider.Current = _container;

            // act
            var decorated = decoratee.DecoratedWith(typeof(DecoratorWithDependency));

            // assert
            Assert.Equal("_decoratorWithDependency_dependency_derived", decorated.Do());
        }
    }
}
