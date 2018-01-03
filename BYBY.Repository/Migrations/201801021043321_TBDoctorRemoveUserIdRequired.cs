namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBDoctorRemoveUserIdRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBDoctors", "UserId", "dbo.TBUsers");
            DropIndex("dbo.TBDoctors", new[] { "UserId" });
            AlterColumn("dbo.TBDoctors", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TBDoctors", "UserId");
            AddForeignKey("dbo.TBDoctors", "UserId", "dbo.TBUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBDoctors", "UserId", "dbo.TBUsers");
            DropIndex("dbo.TBDoctors", new[] { "UserId" });
            AlterColumn("dbo.TBDoctors", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TBDoctors", "UserId");
            AddForeignKey("dbo.TBDoctors", "UserId", "dbo.TBUsers", "Id", cascadeDelete: true);
        }
    }
}
