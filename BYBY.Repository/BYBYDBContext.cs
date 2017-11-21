namespace BYBY.Repository
{
    using BYBY.Repository.Entities;
    using System.Data.Entity;

    public partial class BYBYDBContext : DbContext
    {
        public DbSet<TBUser> TBUsers { get; set; }
        public DbSet<TBRole> TBRoles { get; set; }
        public DbSet<TBUserRole> TBUserRoles { get; set; }

        public DbSet<TBPatient> TBPatients { get; set; }
        public DbSet<TBMedicalHistory> TBMedicalHistorys { get; set; }
        public BYBYDBContext()
            : base("name=conn1")
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TBMedicalHistory>().HasRequired(m => m.MalePatient).WithMany(n => n.MaleMedicalHistorys).HasForeignKey(m => m.MalePatientId).WillCascadeOnDelete(false);
            modelBuilder.Entity<TBMedicalHistory>().HasRequired(m => m.FeMalePatient).WithMany(n => n.FeMaleMedicalHistorys).HasForeignKey(m => m.FeMalePatientId).WillCascadeOnDelete(false);
        }
    }
}
