using System;
using System.Collections.Generic;

namespace CqrsEventSourcingDemo.ReadModel
{
    public interface IReadModelSet<TReadModel> : IEnumerable<TReadModel> where TReadModel : ReadModel
    {
        void Add(TReadModel view);

        void Update(Guid id, Action<TReadModel> updateAction);

        void Remove(Guid id);

        TReadModel GetById(Guid id);
    }
}