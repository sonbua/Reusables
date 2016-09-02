using System;
using MailingServiceDemo.Database;
using MailingServiceDemo.Model;
using Reusables.Diagnostics.Logging;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class DbContextSpy : IDbContext
    {
        private readonly IDbContext _victimDbContext;
        private readonly IServiceProvider _serviceProvider;

        public DbContextSpy(IDbContext victimDbContext, IServiceProvider serviceProvider)
        {
            _victimDbContext = victimDbContext;
            _serviceProvider = serviceProvider;
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            var logger = (ILogger) _serviceProvider.GetService(typeof (ILogger));

            return new DbSetSpy<TEntity>(logger, _victimDbContext.Set<TEntity>());
        }
    }
}