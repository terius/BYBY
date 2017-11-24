namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11223 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBPatients", "HouseholdAddress", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBPatients", "HouseholdAddress");
        }
    }
}
