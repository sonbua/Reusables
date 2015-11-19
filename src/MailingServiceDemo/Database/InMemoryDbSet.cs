using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MailingServiceDemo.Model;

namespace MailingServiceDemo.Database
{
    public class InMemoryDbSet<TEntity> : IDbSet<TEntity>, IEnumerable<TEntity> where TEntity : Entity
    {
        private readonly ICollection<TEntity> _set = new List<TEntity>();

        public void Add(TEntity entity)
        {
            _set.Add(entity);
        }

        public void Update(Guid id, Action<TEntity> updateAction)
        {
            var entity = GetById(id);

            updateAction(entity);
        }

        public void Remove(Guid id)
        {
            _set.Remove(GetById(id));
        }

        public TEntity GetById(Guid id)
        {
            return _set.Single(x => x.Id == id);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _set;
        }
    }
}
