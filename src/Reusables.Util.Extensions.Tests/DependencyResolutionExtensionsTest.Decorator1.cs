namespace Reusables.Util.Extensions.Tests
{
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
}