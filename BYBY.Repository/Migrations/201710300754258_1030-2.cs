namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10302 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBUsers", "SFZ", c => c.String(nullable: false, maxLength: 18));
            AlterColumn("dbo.TBUsers", "Name", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TBUsers", "Name", c => c.String());
            DropColumn("dbo.TBUsers", "SFZ");
        }
    }
}
