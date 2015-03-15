namespace p.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _173716052014 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photographers", "ChatConnectionId", c => c.String());
            DropColumn("dbo.Photographers", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photographers", "Role", c => c.String());
            DropColumn("dbo.Photographers", "ChatConnectionId");
        }
    }
}
