namespace BYBY.Repository.Entities
{
    public class TBUserRole : BaseEntity<int>
    {

        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual TBUser User { get; set; }

        public virtual TBRole Role { get; set; }
    }
}
