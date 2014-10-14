namespace SimpleSoftballStats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Games : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        RunsScored = c.Int(nullable: false),
                        RunsAllowed = c.Int(nullable: false),
                        Opponent = c.String(),
                        GameTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Team", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Game", "TeamId", "dbo.Team");
            DropIndex("dbo.Game", new[] { "TeamId" });
            DropTable("dbo.Game");
        }
    }
}
