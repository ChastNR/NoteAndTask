using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Repositories
{
    public class EfRepository<TContext> : EfReadOnlyRepository<TContext>, IRepository
        where TContext : DbContext
    {
        public EfRepository(TContext context)
            : base(context)
        { }

        public virtual void Create<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(object id)
            where TEntity : class, IEntity
        {
            TEntity entity = Context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = Context.Set<TEntity>();
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public virtual Task SaveAsync() => Context.SaveChangesAsync();
    }
}