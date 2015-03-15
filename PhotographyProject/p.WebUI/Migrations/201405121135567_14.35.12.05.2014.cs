namespace p.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _143512052014 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photographers", "Online", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photographers", "Online");
        }
    }
}
