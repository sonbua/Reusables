namespace Reusables.BuildingBlocks
{
    public interface IHandler<in TInput, out TOutput>
    {
        TOutput Handle(TInput input);
    }
}
