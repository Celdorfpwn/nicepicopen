namespace p.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _040719052014 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photographers", "SmallProfile", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photographers", "SmallProfile");
        }
    }
}
