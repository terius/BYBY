using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYBY.Infrastructure.Domain
{
    public interface IReadOnlyRepository<T, EntityKey> where T : class, IEntity
    {

        Task<T> FindById(EntityKey Id);

        //   IQueryable<T> Query(Query<T> query);

        Task<IEnumerable<T>> FindAll();
        Task<T> FindSingleBy(Func<T, bool> sqlWhere);

        Task<IQueryable<T>> FindBy(Func<T, bool> sqlWhere);

        //   IList<T> PageQuery(Query<T> query,int pageIndex,int pageSize,out int allCount);
    }
}
