namespace SimpleSoftballStats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allownullruns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Game", "RunsScored", c => c.Int());
            AlterColumn("dbo.Game", "RunsAllowed", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Game", "RunsAllowed", c => c.Int(nullable: false));
            AlterColumn("dbo.Game", "RunsScored", c => c.Int(nullable: false));
        }
    }
}
