using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleSoftballStats.Model
{
    public class RosterEntry : IObjectWithState
    {        
        public RosterEntry()
        {
            //Team = new Team();
            //Player = new Player();
        }

        public int TeamId { get; set; }
        public int PlayerId { get; set; }
        public virtual Team Team { get; set; }
        public virtual Player Player { get; set; }

        public ObjectState ObjectState { get; set;}        
    }
}