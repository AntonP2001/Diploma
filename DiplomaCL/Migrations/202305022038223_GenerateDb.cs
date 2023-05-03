namespace DiplomaCL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenerateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catalogues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsSelected = c.Boolean(nullable: false),
                        Password = c.String(),
                        ParentCatalogue_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Catalogues", t => t.ParentCatalogue_Id)
                .Index(t => t.ParentCatalogue_Id);
            
            AddColumn("dbo.Partitures", "Image", c => c.Binary());
            AddColumn("dbo.Partitures", "Catalogue_Id", c => c.Int());
            CreateIndex("dbo.Partitures", "Catalogue_Id");
            AddForeignKey("dbo.Partitures", "Catalogue_Id", "dbo.Catalogues", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Partitures", "Catalogue_Id", "dbo.Catalogues");
            DropForeignKey("dbo.Catalogues", "ParentCatalogue_Id", "dbo.Catalogues");
            DropIndex("dbo.Partitures", new[] { "Catalogue_Id" });
            DropIndex("dbo.Catalogues", new[] { "ParentCatalogue_Id" });
            DropColumn("dbo.Partitures", "Catalogue_Id");
            DropColumn("dbo.Partitures", "Image");
            DropTable("dbo.Catalogues");
        }
    }
}
