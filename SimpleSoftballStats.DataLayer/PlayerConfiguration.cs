using SimpleSoftballStats.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSoftballStats.DataLayer
{
    class PlayerConfiguration : EntityTypeConfiguration<Player>
    {
        public PlayerConfiguration()
        {
            Ignore(o => o.FullName);
            Property(o => o.FirstName).HasMaxLength(20).IsRequired();
            Property(o => o.LastName).HasMaxLength(40).IsOptional();            
        }
    }
}
