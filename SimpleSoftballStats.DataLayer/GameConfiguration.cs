using SimpleSoftballStats.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSoftballStats.DataLayer
{
    class GameConfiguration : EntityTypeConfiguration<Game>
    {
        public GameConfiguration()
        {
            Property(o => o.Opponent).HasMaxLength(30).IsOptional();            
            
            Ignore(o => o.Result);
        }
    }
}
