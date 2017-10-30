namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10303 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBUsers", "AddTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.TBUsers", "ModifyTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.TBUsers", "AddUserName", c => c.String(maxLength: 20));
            AddColumn("dbo.TBUsers", "ModifyUserName", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBUsers", "ModifyUserName");
            DropColumn("dbo.TBUsers", "AddUserName");
            DropColumn("dbo.TBUsers", "ModifyTime");
            DropColumn("dbo.TBUsers", "AddTime");
        }
    }
}
