namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBDoctorAddSexAndBirthday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBDoctors", "Sex", c => c.Int(nullable: false));
            AddColumn("dbo.TBDoctors", "Birthday", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBDoctors", "Birthday");
            DropColumn("dbo.TBDoctors", "Sex");
        }
    }
}
