namespace Assignment9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_field_audio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "Audio", c => c.Binary());
            AddColumn("dbo.Tracks", "AudioContentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "AudioContentType");
            DropColumn("dbo.Tracks", "Audio");
        }
    }
}
