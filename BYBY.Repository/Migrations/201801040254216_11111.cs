namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11111 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TBDoctors", "IsMasterDoctor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TBDoctors", "IsMasterDoctor", c => c.Boolean(nullable: false));
        }
    }
}
