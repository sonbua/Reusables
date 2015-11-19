using System;
using System.Collections.Generic;
using MailingServiceDemo.Model;

namespace MailingServiceDemo.Database
{
    public interface IDbSet<TEntity> : IEnumerable<TEntity> where TEntity : Entity
    {
        void Add(TEntity entity);

        void Update(Guid id, Action<TEntity> updateAction);

        void Remove(Guid id);

        TEntity GetById(Guid id);
    }
}
