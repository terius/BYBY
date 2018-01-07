namespace BYBY.Repository
{
    using BYBY.Repository.Entities;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    public partial class BYBYDBContext : DbContext
    {
        public DbSet<TBUser> TBUsers { get; set; }
        public DbSet<TBRole> TBRoles { get; set; }
        public DbSet<TBUserRole> TBUserRoles { get; set; }

        public DbSet<TBPatient> TBPatients { get; set; }
        public DbSet<TBMedicalHistory> TBMedicalHistorys { get; set; }

        public DbSet<TBMedicalDetail> TBMedicalDetails { get; set; }
        public DbSet<TBHospital> TBHospitals { get; set; }
        public DbSet<TBNation> TBNations { get; set; }
        public DbSet<TBJob> TBJobs { get; set; }
        public DbSet<TBEthnic> TBEthnics { get; set; }

        public DbSet<TBModule> TBModules { get; set; }

        public DbSet<TBMedicalHistoryImage> TBMedicalHistoryImages { get; set; }
        public DbSet<TBConsultation> TBConsultations { get; set; }
        public DbSet<TBDoctor> TBDoctors { get; set; }

        public DbSet<TBReferral> TBReferrals { get; set; }

        public DbSet<TBMedicine> TBMedicines { get; set; }

        public DbSet<TBConsultationMedicine> TBConsultationMedicines { get; set; }

        public DbSet<TBConsultationCheck> TBConsultationChecks { get; set; }

        public DbSet<TBCheckAssay> TBCheckAssays { get; set; }

        public DbSet<TBConsultationRoom> TBConsultationRooms { get; set; }

        public DbSet<TBDateSetup> TBDateSetups { get; set; }

        public DbSet<TBPlan> TBPlans { get; set; }
        public BYBYDBContext()
            : base("name=conn1")
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TBMedicalHistory>()
              .Property(e => e.LandlinePhone)
              .HasMaxLength(50);
            //modelBuilder.Entity<TBUser>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<TBRole>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<TBUserRole>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TBMedicalHistory>().HasRequired(m => m.MalePatient).WithMany(n => n.MaleMedicalHistorys).HasForeignKey(m => m.MalePatientId).WillCascadeOnDelete(false);
            modelBuilder.Entity<TBMedicalHistory>().HasRequired(m => m.FeMalePatient).WithMany(n => n.FeMaleMedicalHistorys).HasForeignKey(m => m.FeMalePatientId).WillCascadeOnDelete(false);

            modelBuilder.Entity<TBHospital>().HasMany(t => t.Doctors).WithRequired(p => p.Hospital).WillCascadeOnDelete(false);
            modelBuilder.Entity<TBPlan>().HasMany(t => t.Consultations).WithRequired(p => p.Plan).WillCascadeOnDelete(false);
        }
    }
}
