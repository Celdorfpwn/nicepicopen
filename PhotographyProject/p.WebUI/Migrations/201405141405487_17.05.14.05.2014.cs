namespace p.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _170514052014 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Photographers", "ProfilePicture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photographers", "ProfilePicture", c => c.Binary());
        }
    }
}
