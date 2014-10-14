using SimpleSoftballStats.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleSoftballStats.ViewModels
{
    public class GameViewModel: IObjectWithState
    {
        public int Id { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public int? RunsScored { get; set; }
        public int? RunsAllowed { get; set; }
        public string Opponent { get; set; }
        public DateTime? GameTime { get; set; }

        public ObjectState ObjectState {get; set;}

        public string MessageToClient { get; set; }

        public List<TeamViewModel> AvailableTeams { get; set; }
        public List<PlayerViewModel> AvailablePlayers { get; set; }

        public List<GameBoxScoreDetailViewModel> BoxScoreDetails { get; set; }
        public List<int> BoxScoreDetailsToDelete { get; set; }

        public GameViewModel()
        {
            AvailableTeams = new List<TeamViewModel>();
            AvailablePlayers = new List<PlayerViewModel>();
            BoxScoreDetails = new List<GameBoxScoreDetailViewModel>();
            BoxScoreDetailsToDelete = new List<int>();
        }
    }
}