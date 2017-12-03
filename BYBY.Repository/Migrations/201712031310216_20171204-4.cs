namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201712044 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TBRoles", "Id", c => c.String(nullable: false, maxLength: 128,defaultValueSql:"NEWID()"));
            AlterColumn("dbo.TBUserRoles", "Id", c => c.String(nullable: false, maxLength: 128, defaultValueSql: "NEWID()"));
            AlterColumn("dbo.TBUsers", "Id", c => c.String(nullable: false, maxLength: 128, defaultValueSql: "NEWID()"));
        }
        
        public override void Down()
        {
        }
    }
}
