﻿using BYBY.Infrastructure.Domain;
using BYBY.Infrastructure.UnitOfWork;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BYBY.Repository.Repositories
{
    public class Repository<T, EntityKey> : ReadOnlyRepository<T, EntityKey>, IRepository<T, EntityKey>, IUnitOfWorkRepository where T : class, IEntity, new()
    {
        private IUnitOfWork _uow;
        //   private readonly DbContext _context;// = new BYBYDBContext();

        public Repository(IUnitOfWork uow)
        {
            _uow = uow;

        }

        public DbSet<T> GetDbSet()
        {
            return DataContextFactory.GetDataContext().Set<T>();
        }

        public IQueryable<T> GetDbSetForEdit()
        {
            return DataContextFactory.GetDataContext().Set<T>();
        }

        public async Task InsertAsync(T entity)
        {
            //  entity.ThrowExceptionIfInvalid(DBAction.Add);
            //try
            //{
            GetDbSet().Add((T)entity);
            await Task.FromResult(0);
            //}
            //catch (Exception ex)
            //{
            //    LoggingFactory.GetLogger().Log(ex.ToString());
            //    throw;
            //}
        }

        public async Task DeleteAsync(T entity)
        {
            GetDbSet().Remove((T)entity);
            await Task.FromResult(0);
            //  _uow.RegisterRemoved(entity, this);
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.FromResult(0);
            //   entity.ThrowExceptionIfInvalid(DBAction.Edit);
            //   _uow.RegisterAmended(entity, this);

            // Do nothing as EF tracks changes
        }

        //public override DbSet<T> GetDbSet()
        //{
        //    return DataContextFactory.GetDataContext().Set<T>();
        //}

        public void PersistCreationOf(IEntity entity)
        {
            GetDbSet().Add((T)entity);
        }

        public void PersistUpdateOf(IEntity entity)
        {

            // Do nothing as EF tracks changes
            // DataContextFactory.GetDataContext().
        }

        public void PersistDeletionOf(IEntity entity)
        {
            GetDbSet().Remove((T)entity);
        }




        //public int RemoveALL()
        //{
        //    int icount = 0;
        //    var list = GetDbSet();
        //    foreach (var item in list)
        //    {
        //        Remove(item);
        //        icount++;
        //    }
        //    return icount;
        //}

        //public int RemoveByWhere(Func<T, bool> wherefun)
        //{
        //    int icount = 0;
        //    GetDbSet().Where(wherefun).ToList().ForEach(d => { Remove(d); icount++; });
        //    return icount;
        //}


    }
}
