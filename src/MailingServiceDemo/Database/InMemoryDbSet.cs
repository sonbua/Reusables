using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MailingServiceDemo.Database
{
    public class InMemoryDbSet<TEntity> : IDbSet<TEntity>, IEnumerable<TEntity> where TEntity : Entity
    {
        private readonly ICollection<TEntity> _viewModels = new List<TEntity>();

        public void Add(TEntity view)
        {
            _viewModels.Add(view);
        }

        public void Update(Guid id, Action<TEntity> updateAction)
        {
            var viewModel = GetById(id);

            updateAction(viewModel);
        }

        public void Remove(Guid id)
        {
            _viewModels.Remove(GetById(id));
        }

        public TEntity GetById(Guid id)
        {
            return _viewModels.Single(x => x.Id == id);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _viewModels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _viewModels;
        }
    }
}
