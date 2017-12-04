namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1223 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBRoles", "Remark", c => c.String(maxLength: 200));
            AlterColumn("dbo.TBRoles", "Name", c => c.String(maxLength: 30));
            AlterColumn("dbo.TBRoles", "DisplayName", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TBRoles", "DisplayName", c => c.String());
            AlterColumn("dbo.TBRoles", "Name", c => c.String());
            DropColumn("dbo.TBRoles", "Remark");
        }
    }
}
