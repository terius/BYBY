using BYBY.Infrastructure.Domain;

namespace BYBY.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        void RegisterAmended(IEntity entity, IUnitOfWorkRepository unitofWorkRepository);
        void RegisterNew(IEntity entity, IUnitOfWorkRepository unitofWorkRepository);
        void RegisterRemoved(IEntity entity, IUnitOfWorkRepository unitofWorkRepository);
        int Commit();
    }

}
