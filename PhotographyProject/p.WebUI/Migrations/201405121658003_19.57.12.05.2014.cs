namespace p.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _195712052014 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.activeConversations",
                c => new
                    {
                        photographerId = c.Int(nullable: false),
                        conversationsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.photographerId, t.conversationsId })
                .ForeignKey("dbo.Photographers", t => t.photographerId, cascadeDelete: true)
                .ForeignKey("dbo.Conversations", t => t.conversationsId, cascadeDelete: true)
                .Index(t => t.photographerId)
                .Index(t => t.conversationsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.activeConversations", "conversationsId", "dbo.Conversations");
            DropForeignKey("dbo.activeConversations", "photographerId", "dbo.Photographers");
            DropIndex("dbo.activeConversations", new[] { "conversationsId" });
            DropIndex("dbo.activeConversations", new[] { "photographerId" });
            DropTable("dbo.activeConversations");
        }
    }
}
