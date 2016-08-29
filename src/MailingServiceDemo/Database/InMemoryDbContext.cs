using System;
using System.Collections.Generic;
using MailingServiceDemo.Model;

namespace MailingServiceDemo.Database
{
    public class InMemoryDbContext : IDbContext
    {
        private readonly Dictionary<Type, object> _tables;

        public InMemoryDbContext()
        {
            _tables = new Dictionary<Type, object>();
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            // TODO: lock-free design
            lock (_tables)
            {
                if (_tables.ContainsKey(typeof (TEntity)))
                    return (IDbSet<TEntity>) _tables[typeof (TEntity)];

                var table = new InMemoryDbSet<TEntity>();

                _tables.Add(typeof (TEntity), table);

                return table;
            }
        }
    }
}
