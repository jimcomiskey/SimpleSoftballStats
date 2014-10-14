using SimpleSoftballStats.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SimpleSoftballStats.DataLayer
{
    public class SoftballContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new GameConfiguration());
            modelBuilder.Configurations.Add(new PlayerConfiguration());
            modelBuilder.Configurations.Add(new RosterEntryConfiguration());
            modelBuilder.Configurations.Add(new GameBoxScoreDetailConfiguration());
            modelBuilder.Configurations.Add(new TeamConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<SimpleSoftballStats.Model.Player> Players { get; set; }
        public DbSet<RosterEntry> RosterEntries { get; set; }

        public System.Data.Entity.DbSet<SimpleSoftballStats.Model.Game> Games { get; set; }
        public DbSet<GameBoxScoreDetail> GameBoxScoreDetails { get; set; }
    }
}