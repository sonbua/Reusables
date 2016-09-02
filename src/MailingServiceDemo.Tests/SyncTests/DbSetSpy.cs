using System;
using System.Collections;
using System.Collections.Generic;
using MailingServiceDemo.Database;
using MailingServiceDemo.Model;
using Reusables.Diagnostics.Logging;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class DbSetSpy<TEntity> : IDbSet<TEntity> where TEntity : Entity
    {
        private readonly ILogger _logger;
        private readonly IDbSet<TEntity> _victimDbSet;

        public DbSetSpy(ILogger logger, IDbSet<TEntity> victimDbSet)
        {
            _logger = logger;
            _victimDbSet = victimDbSet;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _victimDbSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TEntity entity)
        {
            _logger.Info($"{nameof(Add)} {entity.GetType().Name}: {entity.ToJson()}");

            _victimDbSet.Add(entity);
        }

        public void Update(Guid id, Action<TEntity> updateAction)
        {
            _logger.Info($"{nameof(Update)} {typeof (TEntity).Name}: {updateAction.ToJson()}");

            _victimDbSet.Update(id, updateAction);
        }

        public void Remove(Guid id)
        {
            _logger.Info($"{nameof(Remove)} {typeof (TEntity).Name}: {id}");

            _victimDbSet.Remove(id);
        }

        public TEntity GetById(Guid id)
        {
            _logger.Info($"{nameof(GetById)} {typeof (TEntity).Name}: {id}");

            var entity = _victimDbSet.GetById(id);

            _logger.Info($"  > {entity.ToJson()}");

            return entity;
        }
    }
}