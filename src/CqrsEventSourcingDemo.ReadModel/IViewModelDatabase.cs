namespace CqrsEventSourcingDemo.ReadModel
{
    // TODO: rename to IReadModelDatabase
    public interface IViewModelDatabase
    {
        IViewModelSet<TViewModel> Set<TViewModel>() where TViewModel : ViewModel;
    }
}
