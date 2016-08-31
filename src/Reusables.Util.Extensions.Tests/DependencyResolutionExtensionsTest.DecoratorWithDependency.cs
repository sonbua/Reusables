namespace Reusables.Util.Extensions.Tests
{
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
}