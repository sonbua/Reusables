namespace Reusables.BuildingBlocks
{
    public class SequentialHandler<TInput, TTemp, TOutput> : IHandler<TInput, TOutput>
    {
        private readonly IHandler<TInput, TTemp> _firstHandler;
        private readonly IHandler<TTemp, TOutput> _secondHandler;

        public SequentialHandler(IHandler<TInput, TTemp> firstHandler, IHandler<TTemp, TOutput> secondHandler)
        {
            _firstHandler = firstHandler;
            _secondHandler = secondHandler;
        }

        public TOutput Handle(TInput input)
        {
            var temp = _firstHandler.Handle(input);

            return _secondHandler.Handle(temp);
        }
    }
}
