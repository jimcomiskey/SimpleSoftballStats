using SimpleSoftballStats.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleSoftballStats.ViewModels
{
    public class TeamViewModel : IObjectWithState 
    {
        public TeamViewModel()
        {
            RosterEntries = new List<RosterEntryViewModel>();
            AvailablePlayers = new List<PlayerViewModel>();
            RosterEntriesToDelete = new List<int>();
        } 
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public string MessageToClient { get; set; }

        public ObjectState ObjectState { get; set;}

        public List<RosterEntryViewModel> RosterEntries { get; set; }
        public List<int> RosterEntriesToDelete { get; set; }

        public List<PlayerViewModel> AvailablePlayers { get; set; }
        public List<PlayerStatsViewModel> PlayerStats { get; set; }

        public PlayerViewModel PlayerToAdd { get; set; }
    }
}