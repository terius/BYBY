namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20171130 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBPatients", "NativePlace", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBPatients", "NativePlace");
        }
    }
}
