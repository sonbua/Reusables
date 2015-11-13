namespace Reusables.EventSourcing
{
    public interface IViewDatabase
    {
        IViewSet<TView> Set<TView>() where TView : View;
    }
}
