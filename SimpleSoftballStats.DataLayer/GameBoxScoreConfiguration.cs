using SimpleSoftballStats.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSoftballStats.DataLayer
{
    class GameBoxScoreDetailConfiguration : EntityTypeConfiguration<GameBoxScoreDetail>
    {
        public GameBoxScoreDetailConfiguration()
        {
            this.HasKey(k => new { k.GameId, k.PlayerId });
            this.Property(p => p.GameId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).IsRequired();
            this.Property(p => p.PlayerId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).IsRequired();
        }
    }
}
