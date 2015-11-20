using System;
using System.Collections.Generic;
using MailingServiceDemo.Model;

namespace MailingServiceDemo.Database
{
    public class InMemoryDbContext : IDbContext
    {
        private readonly ISet<Type> _cachedTypes;

        public InMemoryDbContext()
        {
            _cachedTypes = new HashSet<Type>();
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            if (_cachedTypes.Contains(typeof (TEntity)))
            {
                return Table<TEntity>.Instance;
            }

            _cachedTypes.Add(typeof (TEntity));
            Table<TEntity>.Instance = null;

            return Table<TEntity>.Instance;
        }

        public void Clean()
        {
            _cachedTypes.Clear();
        }

        private static class Table<TEntity> where TEntity : Entity
        {
            private static IDbSet<TEntity> _instance;

            public static IDbSet<TEntity> Instance
            {
                get { return _instance ?? (_instance = new InMemoryDbSet<TEntity>()); }
                set { _instance = value; }
            }
        }
    }
}
