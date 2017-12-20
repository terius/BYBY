using BYBY.Infrastructure;
using BYBY.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    [Serializable]
    public abstract class BaseEntity<T> : IEntity
    {
        public virtual T Id { get; set; }

        public DateTime? AddTime { get; set; }

        public DateTime? ModifyTime { get; set; }

        [StringLength(20)]
        public string AddUserName { get; set; }
        [StringLength(20)]
        public string ModifyUserName { get; set; }

        [NotMapped]
        public DBAction dbaction { get; set; }

      

        protected virtual void Validate()
        {
            if (dbaction == DBAction.Add)
            {
                AddTime = DateTime.Now;
               
            }
            else if (dbaction == DBAction.Update)
            {
                ModifyTime = DateTime.Now;
            }
        }

        private ErrorMessage _errorMessage;
        protected void AddError(ErrorType errorType, string message)
        {
            _errorMessage = ErrorMessage.Create(errorType, message);
        }

        public void ThrowExceptionIfInvalid(DBAction _action)
        {
            dbaction = _action;
            _errorMessage = null;
            Validate();
            if (_errorMessage != null)
            {
                throw new InsufficientFundsException(_errorMessage.Message);
            }
        }
    }
}
