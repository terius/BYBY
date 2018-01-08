namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableTBMasterHospital2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TBMasterHospitals", name: "MasterHospitalId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.TBMasterHospitals", name: "ChildHospitalId", newName: "MasterHospitalId");
            RenameColumn(table: "dbo.TBMasterHospitals", name: "__mig_tmp__0", newName: "ChildHospitalId");
            RenameIndex(table: "dbo.TBMasterHospitals", name: "IX_MasterHospitalId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.TBMasterHospitals", name: "IX_ChildHospitalId", newName: "IX_MasterHospitalId");
            RenameIndex(table: "dbo.TBMasterHospitals", name: "__mig_tmp__0", newName: "IX_ChildHospitalId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TBMasterHospitals", name: "IX_ChildHospitalId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.TBMasterHospitals", name: "IX_MasterHospitalId", newName: "IX_ChildHospitalId");
            RenameIndex(table: "dbo.TBMasterHospitals", name: "__mig_tmp__0", newName: "IX_MasterHospitalId");
            RenameColumn(table: "dbo.TBMasterHospitals", name: "ChildHospitalId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.TBMasterHospitals", name: "MasterHospitalId", newName: "ChildHospitalId");
            RenameColumn(table: "dbo.TBMasterHospitals", name: "__mig_tmp__0", newName: "MasterHospitalId");
        }
    }
}
