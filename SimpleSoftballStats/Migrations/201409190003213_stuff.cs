namespace SimpleSoftballStats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stuff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RosterEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.PlayerId);
            
            AlterColumn("dbo.Player", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Player", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Team", "TeamName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RosterEntry", "TeamId", "dbo.Team");
            DropForeignKey("dbo.RosterEntry", "PlayerId", "dbo.Player");
            DropIndex("dbo.RosterEntry", new[] { "PlayerId" });
            DropIndex("dbo.RosterEntry", new[] { "TeamId" });
            AlterColumn("dbo.Team", "TeamName", c => c.String());
            AlterColumn("dbo.Player", "LastName", c => c.String());
            AlterColumn("dbo.Player", "FirstName", c => c.String());
            DropTable("dbo.RosterEntry");
        }
    }
}
