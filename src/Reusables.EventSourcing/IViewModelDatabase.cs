namespace Reusables.EventSourcing
{
    public interface IViewModelDatabase
    {
        IViewModelSet<TViewModel> Set<TViewModel>() where TViewModel : ViewModel;
    }
}
