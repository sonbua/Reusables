using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CqrsEventSourcingDemo.ReadModel;

namespace CqrsEventSourcingDemo.Infrastructure
{
    public class InMemoryReadModelSet<TReadModel> : IReadModelSet<TReadModel> where TReadModel : ReadModel.ReadModel
    {
        private readonly ICollection<TReadModel> _viewModels = new List<TReadModel>();

        public void Add(TReadModel view)
        {
            _viewModels.Add(view);
        }

        public void Update(Guid id, Action<TReadModel> updateAction)
        {
            var viewModel = GetById(id);

            updateAction(viewModel);
        }

        public void Remove(Guid id)
        {
            _viewModels.Remove(GetById(id));
        }

        public TReadModel GetById(Guid id)
        {
            return _viewModels.Single(x => x.Id == id);
        }

        public IEnumerator<TReadModel> GetEnumerator()
        {
            return _viewModels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<TReadModel> GetAll()
        {
            return _viewModels;
        }
    }
}
