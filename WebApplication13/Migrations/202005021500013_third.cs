namespace WebApplication13.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Userapps", "phone", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Userapps", "country", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Userapps", "country", c => c.String(nullable: false));
            AlterColumn("dbo.Userapps", "phone", c => c.String(nullable: false));
        }
    }
}
