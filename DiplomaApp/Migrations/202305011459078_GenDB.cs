namespace DiplomaUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenDB : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Catalogues", new[] { "CatalogueId" });
            RenameColumn(table: "dbo.Catalogues", name: "CatalogueId", newName: "ParentCatalogue_Id");
            RenameColumn(table: "dbo.Partitures", name: "CatalogueId", newName: "Catalogue_Id");
            RenameIndex(table: "dbo.Partitures", name: "IX_CatalogueId", newName: "IX_Catalogue_Id");
            AddColumn("dbo.Users", "FullName", c => c.String());
            AddColumn("dbo.Users", "CourseNum", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "AccessLevel", c => c.Int(nullable: false));
            AlterColumn("dbo.Catalogues", "ParentCatalogue_Id", c => c.Int());
            CreateIndex("dbo.Catalogues", "ParentCatalogue_Id");
            DropColumn("dbo.Users", "Name");
            DropColumn("dbo.Users", "Course");
            DropColumn("dbo.Users", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "Course", c => c.Short(nullable: false));
            AddColumn("dbo.Users", "Name", c => c.String());
            DropIndex("dbo.Catalogues", new[] { "ParentCatalogue_Id" });
            AlterColumn("dbo.Catalogues", "ParentCatalogue_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "AccessLevel");
            DropColumn("dbo.Users", "CourseNum");
            DropColumn("dbo.Users", "FullName");
            RenameIndex(table: "dbo.Partitures", name: "IX_Catalogue_Id", newName: "IX_CatalogueId");
            RenameColumn(table: "dbo.Partitures", name: "Catalogue_Id", newName: "CatalogueId");
            RenameColumn(table: "dbo.Catalogues", name: "ParentCatalogue_Id", newName: "CatalogueId");
            CreateIndex("dbo.Catalogues", "CatalogueId");
        }
    }
}
