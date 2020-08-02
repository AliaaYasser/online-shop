namespace WebApplication13.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSnapshot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        categoryid = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.categoryid);
            
            CreateTable(
                "dbo.products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        price = c.Single(nullable: false),
                        Description = c.String(),
                        image = c.String(),
                        categoryid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.categories", t => t.categoryid, cascadeDelete: true)
                .Index(t => t.categoryid);
            
            CreateTable(
                "dbo.Userapps",
                c => new
                    {
                        userId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        phone = c.String(nullable: false),
                        country = c.String(nullable: false),
                        image = c.String(nullable: false),
                        uR = c.String(),
                    })
                .PrimaryKey(t => t.userId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Userappproducts",
                c => new
                    {
                        Userapp_userId = c.Int(nullable: false),
                        product_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Userapp_userId, t.product_id })
                .ForeignKey("dbo.Userapps", t => t.Userapp_userId, cascadeDelete: true)
                .ForeignKey("dbo.products", t => t.product_id, cascadeDelete: true)
                .Index(t => t.Userapp_userId)
                .Index(t => t.product_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Userappproducts", "product_id", "dbo.products");
            DropForeignKey("dbo.Userappproducts", "Userapp_userId", "dbo.Userapps");
            DropForeignKey("dbo.products", "categoryid", "dbo.categories");
            DropIndex("dbo.Userappproducts", new[] { "product_id" });
            DropIndex("dbo.Userappproducts", new[] { "Userapp_userId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.products", new[] { "categoryid" });
            DropTable("dbo.Userappproducts");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Userapps");
            DropTable("dbo.products");
            DropTable("dbo.categories");
        }
    }
}
