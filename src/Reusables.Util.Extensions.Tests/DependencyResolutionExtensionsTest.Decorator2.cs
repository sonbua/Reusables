namespace Reusables.Util.Extensions.Tests
{
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
}