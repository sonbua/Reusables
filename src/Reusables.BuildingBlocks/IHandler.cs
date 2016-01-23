namespace Reusables.BuildingBlocks
{
    public interface IHandler<TInput, TOutput>
    {
        TOutput Handle(TInput input);
    }
}
