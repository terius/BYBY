namespace BYBY.Infrastructure.Domain
{
    public interface IEntity
    {
        //T Id { get; set; }
        void ThrowExceptionIfInvalid(DBAction action);
    }

}
