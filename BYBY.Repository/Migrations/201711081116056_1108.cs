namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1108 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TBUsers", "Password", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TBUsers", "Password", c => c.String(nullable: false, maxLength: 32));
        }
    }
}
