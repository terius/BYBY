namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableTBMedicine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBMedicines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 100),
                        Type = c.String(nullable: false, maxLength: 100),
                        ShortName = c.String(nullable: false, maxLength: 100),
                        Spec = c.String(maxLength: 100),
                        Dose = c.Int(nullable: false),
                        Unit = c.String(maxLength: 100),
                        Frequency = c.String(maxLength: 100),
                        Instructions = c.String(maxLength: 200),
                        DefaultUsedDay = c.Int(nullable: false),
                        InjectionMark = c.Boolean(nullable: false),
                        IsUsed = c.Boolean(nullable: false),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TBMedicines");
        }
    }
}
