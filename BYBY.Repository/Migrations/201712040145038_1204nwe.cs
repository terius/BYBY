namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1204nwe : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TBPatients", "ContactPhone", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TBPatients", "ContactPhone", c => c.String());
        }
    }
}
