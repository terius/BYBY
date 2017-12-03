using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBRole : BaseEntity<string>, IRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }
        public string Name { get; set; }
       
        public string DisplayName { get; set; }
    }
}
