namespace DiplomaUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenewedDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catalogues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        IsSelected = c.Boolean(nullable: false),
                        Catalogue_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Catalogues", t => t.Catalogue_Id)
                .Index(t => t.Catalogue_Id);
            
            CreateTable(
                "dbo.Partitures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        Name = c.String(),
                        Style = c.String(),
                        WorkType = c.String(),
                        Instrumentation = c.String(),
                        Language = c.String(),
                        File = c.Binary(),
                        Image = c.Binary(),
                        Catalogue_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Catalogues", t => t.Catalogue_Id)
                .Index(t => t.Catalogue_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        CourseNum = c.Int(nullable: false),
                        Login = c.String(),
                        Password = c.String(),
                        AccessLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Partitures", "Catalogue_Id", "dbo.Catalogues");
            DropForeignKey("dbo.Catalogues", "Catalogue_Id", "dbo.Catalogues");
            DropIndex("dbo.Partitures", new[] { "Catalogue_Id" });
            DropIndex("dbo.Catalogues", new[] { "Catalogue_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Partitures");
            DropTable("dbo.Catalogues");
        }
    }
}
