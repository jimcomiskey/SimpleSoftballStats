using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleSoftballStats.Model
{
    public class Team : IObjectWithState
    {
        public Team()
        {
            RosterEntries = new List<RosterEntry>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public virtual ICollection<RosterEntry> RosterEntries { get; set; }

        public ObjectState ObjectState { get; set;}        
    }
}