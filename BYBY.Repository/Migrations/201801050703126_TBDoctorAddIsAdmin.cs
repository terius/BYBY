namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBDoctorAddIsAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBDoctors", "IsAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBDoctors", "IsAdmin");
        }
    }
}
