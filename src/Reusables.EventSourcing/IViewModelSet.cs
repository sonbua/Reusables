using System;
using System.Collections.Generic;

namespace Reusables.EventSourcing
{
    public interface IViewModelSet<TView> : IEnumerable<TView> where TView : ViewModel
    {
        void Add(TView view);

        void Update(Guid id, Action<TView> updateAction);

        void Remove(Guid id);

        TView GetById(Guid id);
    }
}
