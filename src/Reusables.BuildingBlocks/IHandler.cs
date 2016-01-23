namespace Reusables.BuildingBlocks
{
    public interface IHandler<in TMessage, out TOutput>
    {
        TOutput Handle(TMessage message);
    }
}
