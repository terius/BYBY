namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11222 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBEthnics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TBJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TBNations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Chinese = c.String(nullable: false, maxLength: 20),
                        English = c.String(nullable: false, maxLength: 30),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TBPatients", "ContactPhone", c => c.String());
            AddColumn("dbo.TBPatients", "NationaId", c => c.Int());
            AddColumn("dbo.TBPatients", "JobId", c => c.Int());
            AddColumn("dbo.TBPatients", "EthnicId", c => c.Int());
            AddColumn("dbo.TBMedicalHistories", "LandlinePhone", c => c.String(maxLength: 30));
            AddColumn("dbo.TBMedicalHistories", "Address", c => c.String(maxLength: 100));
            AddColumn("dbo.TBMedicalHistories", "Remark", c => c.String(maxLength: 200));
            CreateIndex("dbo.TBPatients", "NationaId");
            CreateIndex("dbo.TBPatients", "JobId");
            CreateIndex("dbo.TBPatients", "EthnicId");
            AddForeignKey("dbo.TBPatients", "EthnicId", "dbo.TBEthnics", "Id");
            AddForeignKey("dbo.TBPatients", "JobId", "dbo.TBJobs", "Id");
            AddForeignKey("dbo.TBPatients", "NationaId", "dbo.TBNations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBPatients", "NationaId", "dbo.TBNations");
            DropForeignKey("dbo.TBPatients", "JobId", "dbo.TBJobs");
            DropForeignKey("dbo.TBPatients", "EthnicId", "dbo.TBEthnics");
            DropIndex("dbo.TBPatients", new[] { "EthnicId" });
            DropIndex("dbo.TBPatients", new[] { "JobId" });
            DropIndex("dbo.TBPatients", new[] { "NationaId" });
            DropColumn("dbo.TBMedicalHistories", "Remark");
            DropColumn("dbo.TBMedicalHistories", "Address");
            DropColumn("dbo.TBMedicalHistories", "LandlinePhone");
            DropColumn("dbo.TBPatients", "EthnicId");
            DropColumn("dbo.TBPatients", "JobId");
            DropColumn("dbo.TBPatients", "NationaId");
            DropColumn("dbo.TBPatients", "ContactPhone");
            DropTable("dbo.TBNations");
            DropTable("dbo.TBJobs");
            DropTable("dbo.TBEthnics");
        }
    }
}
