namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {            
            AlterColumn("dbo.Customers", "MembershipTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.MembershipTypes", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Genres", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Movies", "GenreId", c => c.Int(nullable: false));            
        }
        
        public override void Down()
        {            
            AlterColumn("dbo.Movies", "GenreId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Genres", "Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.MembershipTypes", "Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.Customers", "MembershipTypeId", c => c.Byte(nullable: false));            
        }
    }
}
