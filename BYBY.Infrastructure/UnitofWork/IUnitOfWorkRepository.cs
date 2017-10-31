using BYBY.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BYBY.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        void PersistCreationOf(IEntity entity);
        void PersistUpdateOf(IEntity entity);
        void PersistDeletionOf(IEntity entity);
    }
}
