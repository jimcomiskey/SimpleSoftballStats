using SimpleSoftballStats.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSoftballStats.DataLayer
{
    class TeamConfiguration : EntityTypeConfiguration<Team>
    {
        public TeamConfiguration()
        {            
            Ignore(t => t.ObjectState);
        }
    }
}
