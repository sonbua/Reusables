using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using MailingServiceDemo.Model;

namespace MailingServiceDemo.Database
{
    // TODO: lock-free design
    public class InMemoryDbSet<TEntity> : IDbSet<TEntity> where TEntity : Entity
    {
        private readonly IDictionary<Guid, TEntity> _set = new ConcurrentDictionary<Guid, TEntity>();

        public void Add(TEntity entity)
        {
            if (!_set.ContainsKey(entity.Id))
            {
                _set.Add(entity.Id, entity);
            }
        }

        public void Update(Guid id, Action<TEntity> updateAction)
        {
            var entity = GetById(id);

            updateAction(entity);
        }

        public void Remove(Guid id)
        {
            _set.Remove(id);
        }

        public TEntity GetById(Guid id)
        {
            return _set[id];
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _set.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _set.Values;
        }
    }
}
