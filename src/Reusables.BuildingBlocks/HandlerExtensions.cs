namespace Reusables.BuildingBlocks
{
    public static class HandlerExtensions
    {
        public static IHandler<TInput, TOutput> FollowedBy<TInput, TTemp, TOutput>(this IHandler<TInput, TTemp> firstHandler, IHandler<TTemp, TOutput> secondHandler)
        {
            return new SequentialHandler<TInput, TTemp, TOutput>(firstHandler, secondHandler);
        }
    }
}
