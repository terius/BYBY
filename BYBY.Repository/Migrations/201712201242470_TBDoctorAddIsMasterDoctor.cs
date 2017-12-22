namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBDoctorAddIsMasterDoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBDoctors", "IsMasterDoctor", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBDoctors", "IsMasterDoctor");
        }
    }
}
