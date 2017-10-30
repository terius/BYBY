using System;
using System.ComponentModel.DataAnnotations;

namespace BYBY.Repository.Entities
{
    [Serializable]
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }

        public DateTime? AddTime { get; set; }

        public DateTime? ModifyTime { get; set; }

        [StringLength(20)]
        public string AddUserName { get; set; }
        [StringLength(20)]
        public string ModifyUserName { get; set; }
    }
}
