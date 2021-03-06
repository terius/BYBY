﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BYBY.Infrastructure.Domain
{
    public interface IReadOnlyRepository<T, EntityKey> where T : class, IEntity
    {
        IQueryable<T> GetDbQuerySet();
        Task<T> GetAsync(EntityKey Id);
        Task<List<T>> FindAllAsync();
        Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IOrderedQueryable<T>> FindOrderByAsync<Tkey>(Expression<Func<T, bool>> predicate, Expression<Func<T, Tkey>> orderby);

        Task<int> FindCountAsync(Expression<Func<T, bool>> predicate);

        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);

    }
}
