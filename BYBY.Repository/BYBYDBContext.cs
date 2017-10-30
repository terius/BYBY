namespace BYBY.Repository
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using BYBY.Repository.Entities;

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
