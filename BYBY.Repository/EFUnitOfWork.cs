﻿using BYBY.Infrastructure.Domain;
using BYBY.Infrastructure.Loger;
using BYBY.Infrastructure.UnitOfWork;
using System;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace BYBY.Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public int Commit()
        {
            int rs = 0;
            try
            {
                rs = DataContextFactory.GetDataContext().SaveChanges();
         

            }
            catch (DbUpdateConcurrencyException ex2)
            {
               // LoggingFactory.GetLogger().Log(ex2);
                throw new Exception("当前信息已被他人修改，请重新打开此页面!");
            }
            catch (OptimisticConcurrencyException oex)
            {
              //  LoggingFactory.GetLogger().Log(oex);
                throw new Exception("当前信息已被他人修改，请重新打开此页面");
            }
            catch (DbEntityValidationException dbEx)
            {
                LoggingFactory.GetLogger().Log(dbEx);
                throw;
            }
            //catch (System.Data.Entity.Core.UpdateException uex)
            //{
            //    LoggingFactory.GetLogger().Log(uex);
            //    if (uex.HResult == -2146233087)
            //    {
            //        throw new Exception("您所要删除的记录已用在其他信息中，不可直接删除！");
            //    }
            //    else
            //    {

            //        throw uex;
            //    }

            //}
            catch (Exception ex)
            {
              
              //  LoggingFactory.GetLogger().Log("保存时处理" + ex.ToString());
                throw;
            }

            return rs;
        }

        //public void RegisterAmended(IEntity entity, IUnitOfWorkRepository unitofWorkRepository)
        //{
        //    unitofWorkRepository.PersistUpdateOf(entity);
        //}

        //public void RegisterNew(IEntity entity, IUnitOfWorkRepository unitofWorkRepository)
        //{
        //    unitofWorkRepository.PersistCreationOf(entity);
        //}

        //public void RegisterRemoved(IEntity entity, IUnitOfWorkRepository unitofWorkRepository)
        //{
        //    unitofWorkRepository.PersistDeletionOf(entity);
        //}
    }
}
