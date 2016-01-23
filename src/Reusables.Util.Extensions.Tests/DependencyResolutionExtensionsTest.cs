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
            DependencyResolutionExtensions.ServiceProvider = _container;

            // act
            var decorated = decoratee.DecoratedWith(typeof(DecoratorWithDependency));

            // assert
            Assert.Equal("_decoratorWithDependency_dependency_derived", decorated.Do());
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

    internal class DecoratorWithDependency : IBase
    {
        private readonly IBase _decoratee;
        private readonly IDependency _dependency;

        public DecoratorWithDependency(IBase decoratee, IDependency dependency)
        {
            _decoratee = decoratee;
            _dependency = dependency;
        }

        public string Do()
        {
            return "_decoratorWithDependency" + _dependency.Do() + _decoratee.Do();
        }
    }

    internal interface IDependency
    {
        string Do();
    }

    internal class Dependency : IDependency
    {
        public string Do()
        {
            return "_dependency";
        }
    }

    internal class NonCompatible
    {
    }
}
