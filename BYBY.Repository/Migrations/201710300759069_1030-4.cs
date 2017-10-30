namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10304 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TBUsers", "AddTime", c => c.DateTime());
            AlterColumn("dbo.TBUsers", "ModifyTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TBUsers", "ModifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TBUsers", "AddTime", c => c.DateTime(nullable: false));
        }
    }
}
