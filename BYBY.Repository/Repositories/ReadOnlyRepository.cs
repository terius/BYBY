using BYBY.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BYBY.Repository.Repositories
{

    public class ReadOnlyRepository<T, EntityKey> : IReadOnlyRepository<T, EntityKey> where T : class, IEntity
    {
        public IQueryable<T> GetDbQuerySet()
        {
            return DataContextFactory.GetDataContext().Set<T>().AsNoTracking();
        }

        public virtual Task<T> GetAsync(EntityKey Id)
        {
            Contract.Requires(Id != null);
            return DataContextFactory.GetDataContext().Set<T>().FindAsync(Id);
        }

        public virtual Task<List<T>> FindAllAsync()
        {
            return GetDbQuerySet().ToListAsync();
        }

        public virtual Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return Task.FromResult(GetDbQuerySet().Where(predicate).FirstOrDefault());
        }

        public virtual Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return Task.FromResult(GetDbQuerySet().Where(predicate));
        }

        public virtual Task<int> FindCount(Expression<Func<T, bool>> predicate)
        {
            return Task.FromResult(GetDbQuerySet().Where(predicate).Count());

        }

    }
}
