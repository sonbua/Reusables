using System;
using SimpleInjector;
using Xunit;

namespace Reusables.Util.Extensions.Tests
{
    public class DependencyResolutionExtensions_DecoratedWith_Test
    {
        private readonly Container _container;

        public DependencyResolutionExtensions_DecoratedWith_Test()
        {
            _container = new Container();

            _container.Register<IDependency, Dependency>();

            _container.Verify();
        }

        [Fact]
        public void GivenNonCompatibleDecoratorType_ThrowsException()
        {
            // arrange
            var decoratee = new Derived();

            // act
            Action nonCompatibleDecoratorType = () => decoratee.DecoratedWith(typeof(NonCompatible));

            // assert
            Assert.Throws<ArgumentException>(nonCompatibleDecoratorType);
        }

        [Fact]
        public void GivenOneDecorator_ReturnsCorrectResult()
        {
            // arrange
            var decoratee = new Derived();

            // act
            var decorated = ((IBase) decoratee).DecoratedWith(typeof(Decorator1));

            // assert
            Assert.Equal("_decorator1_derived", decorated.Do());
        }

        [Fact]
        public void GivenTwoDecorators_ReturnsCorrectResult()
        {
            // arrange
            IBase decoratee = new Derived();

            // act
            var decorated = decoratee.DecoratedWith(typeof(Decorator1)).DecoratedWith(typeof(Decorator2));

            // assert
            Assert.Equal("_decorator2_decorator1_derived", decorated.Do());
        }

        [Fact]
        public void GivenDecoratorWithDependency_ReturnsCorrectResult()
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