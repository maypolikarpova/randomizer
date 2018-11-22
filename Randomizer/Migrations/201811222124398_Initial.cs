namespace Randomizer.DBAdapter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        StartNumber = c.Int(nullable: false),
                        EndNumber = c.Int(nullable: false),
                        GeneratedAmount = c.Int(nullable: false),
                        Sequence = c.String(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Users", t => t.UserGuid, cascadeDelete: true)
                .Index(t => t.UserGuid);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        LastLoginDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "UserGuid", "dbo.Users");
            DropIndex("dbo.Request", new[] { "UserGuid" });
            DropTable("dbo.Users");
            DropTable("dbo.Request");
        }
    }
}
