namespace Assignment8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Artists", "BirthName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Artists", "BirthName", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
