namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorAddColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBDoctors", "Remark", c => c.String(maxLength: 500));
            AddColumn("dbo.TBDoctors", "Phone", c => c.String(maxLength: 100));
            AddColumn("dbo.TBDoctors", "Address", c => c.String(maxLength: 300));
            AddColumn("dbo.TBDoctors", "ImageUrl", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBDoctors", "ImageUrl");
            DropColumn("dbo.TBDoctors", "Address");
            DropColumn("dbo.TBDoctors", "Phone");
            DropColumn("dbo.TBDoctors", "Remark");
        }
    }
}
