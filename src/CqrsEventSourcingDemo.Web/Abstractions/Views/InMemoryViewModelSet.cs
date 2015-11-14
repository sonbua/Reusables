using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Abstractions.Views
{
    public class InMemoryViewModelSet<TViewModel> : IViewModelSet<TViewModel>, IEnumerable<TViewModel> where TViewModel : ViewModel
    {
        private readonly ICollection<TViewModel> _viewModels = new List<TViewModel>();

        public void Add(TViewModel view)
        {
            _viewModels.Add(view);
        }

        public void Update(Guid id, Action<TViewModel> updateAction)
        {
            var view = _viewModels.Single(x => x.Id == id);

            updateAction(view);
        }

        public void Remove(Guid id)
        {
            _viewModels.Remove(GetById(id));
        }

        public TViewModel GetById(Guid id)
        {
            return _viewModels.Single(x => x.Id == id);
        }

        public IEnumerator<TViewModel> GetEnumerator()
        {
            return _viewModels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<TViewModel> GetAll()
        {
            return _viewModels;
        }
    }
}
