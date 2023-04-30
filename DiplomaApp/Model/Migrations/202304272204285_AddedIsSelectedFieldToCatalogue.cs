namespace DiplomaCL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsSelectedFieldToCatalogue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catalogues", "IsSelected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catalogues", "IsSelected");
        }
    }
}
