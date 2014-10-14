namespace SimpleSoftballStats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropid : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RosterEntry", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RosterEntry", "Id", c => c.Int(nullable: false));
        }
    }
}
