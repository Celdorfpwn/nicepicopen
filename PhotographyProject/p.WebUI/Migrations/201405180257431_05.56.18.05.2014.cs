namespace p.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _055618052014 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pictures", "ImageToBase64", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pictures", "ImageToBase64");
        }
    }
}
