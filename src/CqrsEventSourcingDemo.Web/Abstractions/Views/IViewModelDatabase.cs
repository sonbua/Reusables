namespace CqrsEventSourcingDemo.Web.Abstractions.Views
{
    public interface IViewModelDatabase
    {
        IViewModelSet<TViewModel> Set<TViewModel>() where TViewModel : ViewModel;
    }
}
