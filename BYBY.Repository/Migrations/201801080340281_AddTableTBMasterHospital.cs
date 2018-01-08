namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableTBMasterHospital : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBHospitals", "ParentHospitalId", "dbo.TBHospitals");
            DropIndex("dbo.TBHospitals", new[] { "ParentHospitalId" });
            CreateTable(
                "dbo.TBMasterHospitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChildHospitalId = c.Int(nullable: false),
                        MasterHospitalId = c.Int(nullable: false),
                        Remark = c.String(maxLength: 500),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBHospitals", t => t.ChildHospitalId)
                .ForeignKey("dbo.TBHospitals", t => t.MasterHospitalId)
                .Index(t => t.ChildHospitalId)
                .Index(t => t.MasterHospitalId);
            
            DropColumn("dbo.TBHospitals", "ParentHospitalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TBHospitals", "ParentHospitalId", c => c.Int());
            DropForeignKey("dbo.TBMasterHospitals", "MasterHospitalId", "dbo.TBHospitals");
            DropForeignKey("dbo.TBMasterHospitals", "ChildHospitalId", "dbo.TBHospitals");
            DropIndex("dbo.TBMasterHospitals", new[] { "MasterHospitalId" });
            DropIndex("dbo.TBMasterHospitals", new[] { "ChildHospitalId" });
            DropTable("dbo.TBMasterHospitals");
            CreateIndex("dbo.TBHospitals", "ParentHospitalId");
            AddForeignKey("dbo.TBHospitals", "ParentHospitalId", "dbo.TBHospitals", "Id");
        }
    }
}
