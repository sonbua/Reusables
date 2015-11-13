using System;
using System.Collections.Generic;

namespace Reusables.EventSourcing
{
    public interface IViewSet<TView> : IEnumerable<TView> where TView : View
    {
        void Add(TView view);

        void Update(Guid id, Action<TView> updateAction);

        void Remove(Guid id);

        TView GetById(Guid id);
    }
}
