﻿namespace DiplomaCL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01Migration : DbMigration
    {
        public override void Up()
        {
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
            DropTable("dbo.Users");
        }
    }
}
