namespace BYBY.Repository
{
    using BYBY.Repository.Entities;
    using System.Data.Entity;

    public partial class BYBYDBContext : DbContext
    {
        public DbSet<TBUser> TBUsers { get; set; }
        public BYBYDBContext()
            : base("name=conn1")
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
