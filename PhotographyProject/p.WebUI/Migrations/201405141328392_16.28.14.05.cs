namespace p.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16281405 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfilePictures",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Photographers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfilePictures", "UserId", "dbo.Photographers");
            DropIndex("dbo.UserProfilePictures", new[] { "UserId" });
            DropTable("dbo.UserProfilePictures");
        }
    }
}
