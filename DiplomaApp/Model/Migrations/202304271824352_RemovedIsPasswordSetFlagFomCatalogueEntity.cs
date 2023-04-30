namespace DiplomaCL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedIsPasswordSetFlagFomCatalogueEntity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Catalogues", "IsPasswordSet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Catalogues", "IsPasswordSet", c => c.Boolean(nullable: false));
        }
    }
}
