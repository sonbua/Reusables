using MailingServiceDemo.Model;

namespace MailingServiceDemo.Database
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : Entity;
    }
}
