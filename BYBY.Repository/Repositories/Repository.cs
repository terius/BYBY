using BYBY.Infrastructure;
using BYBY.Infrastructure.Domain;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BYBY.Repository.Repositories
{
    public class Repository<T, EntityKey> : ReadOnlyRepository<T, EntityKey>, IRepository<T, EntityKey> where T : class, IEntity, new()
    {
        //private DbContext _context
        //{
        //    get
        //    {
        //        return DataContextFactory.GetDataContext();
        //    }
        //}
        public virtual Task InsertAsync(T entity)
        {
            entity.ThrowExceptionIfInvalid(DBAction.Add);
            DataContextFactory.GetDataContext().Set<T>().Add(entity);



            return Task.FromResult(0);
        }

        public virtual Task DeleteAsync(T entity)
        {
            entity.ThrowExceptionIfInvalid(DBAction.Delete);
            DataContextFactory.GetDataContext().Set<T>().Remove(entity);
            return Task.FromResult(0);
        }

        public virtual Task UpdateAsync(T entity)
        {
            entity.ThrowExceptionIfInvalid(DBAction.Update);
            return Task.FromResult(0);
        }

    }
}
