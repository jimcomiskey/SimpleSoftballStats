using SimpleSoftballStats.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSoftballStats.DataLayer
{
    class RosterEntryConfiguration : EntityTypeConfiguration<RosterEntry>
    {
        public RosterEntryConfiguration()
        {
            this.HasKey(k => new { k.TeamId, k.PlayerId });
            this.Property(p => p.TeamId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None).IsRequired();
            this.Property(p => p.TeamId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None).IsRequired();

        }
    }
}
