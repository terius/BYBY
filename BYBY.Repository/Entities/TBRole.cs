using Microsoft.AspNet.Identity;

namespace BYBY.Repository.Entities
{
    public class TBRole : BaseEntity<int>, IRole<int>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }
    }
}
