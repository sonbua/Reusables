namespace CqrsEventSourcingDemo.ReadModel
{
    public interface IViewModelDatabase
    {
        IViewModelSet<TViewModel> Set<TViewModel>() where TViewModel : ViewModel;
    }
}
