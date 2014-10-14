using SimpleSoftballStats.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleSoftballStats.ViewModels
{
    public class RosterEntryViewModel : IObjectWithState
    {
        public int TeamId { get; set; }
        public int PlayerId { get; set; }

        public PlayerViewModel Player { get; set; }
        
        public ObjectState ObjectState { get; set; }        
    }
}