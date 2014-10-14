namespace SimpleSoftballStats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class compositekey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.RosterEntry");
            AlterColumn("dbo.RosterEntry", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.RosterEntry", new[] { "TeamId", "PlayerId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RosterEntry");
            AlterColumn("dbo.RosterEntry", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.RosterEntry", "Id");
        }
    }
}
