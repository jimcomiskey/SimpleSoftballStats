namespace SimpleSoftballStats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boxscore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameBoxScoreDetail",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        BattingOrder = c.Int(nullable: false),
                        PlateAppearances = c.Int(nullable: false),
                        RunsScored = c.Int(nullable: false),
                        Hits = c.Int(nullable: false),
                        Doubles = c.Int(nullable: false),
                        Triples = c.Int(nullable: false),
                        HomeRuns = c.Int(nullable: false),
                        Walks = c.Int(nullable: false),
                        RunsBattedIn = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameId, t.PlayerId })
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameBoxScoreDetail", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.GameBoxScoreDetail", "GameId", "dbo.Game");
            DropIndex("dbo.GameBoxScoreDetail", new[] { "PlayerId" });
            DropIndex("dbo.GameBoxScoreDetail", new[] { "GameId" });
            DropTable("dbo.GameBoxScoreDetail");
        }
    }
}
