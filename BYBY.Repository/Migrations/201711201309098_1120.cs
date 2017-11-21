namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1120 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBPatients", "Sex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBPatients", "Sex");
        }
    }
}
