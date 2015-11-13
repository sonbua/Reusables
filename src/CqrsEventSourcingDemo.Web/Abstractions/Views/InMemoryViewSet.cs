using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Abstractions.Views
{
    public class InMemoryViewSet<TView> : IViewSet<TView>, IEnumerable<TView> where TView : View
    {
        private readonly ICollection<TView> _views = new List<TView>();

        public void Add(TView view)
        {
            _views.Add(view);
        }

        public void Update(Guid id, Action<TView> updateAction)
        {
            var view = _views.Single(x => x.Id == id);

            updateAction(view);
        }

        public void Remove(Guid id)
        {
            _views.Remove(GetById(id));
        }

        public TView GetById(Guid id)
        {
            return _views.Single(x => x.Id == id);
        }

        public IEnumerator<TView> GetEnumerator()
        {
            return _views.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<TView> GetAll()
        {
            return _views;
        }
    }
}
