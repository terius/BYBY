using System.Collections.Generic;

namespace BYBY.Infrastructure.Domain
{
    public interface IReadOnlyRepository<T, EntityKey> where T : class, IEntity
    {

        T Single(EntityKey Id);

        //   IQueryable<T> Query(Query<T> query);

        IEnumerable<T> FindAll();

        //   IList<T> PageQuery(Query<T> query,int pageIndex,int pageSize,out int allCount);
    }
}
