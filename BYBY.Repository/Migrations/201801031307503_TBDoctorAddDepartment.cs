namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBDoctorAddDepartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBDoctors", "Department", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBDoctors", "Department");
        }
    }
}
