namespace p.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _114807052014 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "LastMessageDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "LastMessageDate");
        }
    }
}
