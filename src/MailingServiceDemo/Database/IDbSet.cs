using System;
using System.Collections.Generic;

namespace MailingServiceDemo.Database
{
    public interface IDbSet<TEntity> : IEnumerable<TEntity> where TEntity : Entity
    {
        void Add(TEntity view);

        void Update(Guid id, Action<TEntity> updateAction);

        void Remove(Guid id);

        TEntity GetById(Guid id);
    }
}
