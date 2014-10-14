using SimpleSoftballStats.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleSoftballStats.ViewModels
{
    public class GameBoxScoreDetailViewModel : IObjectWithState
    {
        public int GameId { get; set; }        
        public int PlayerId { get; set; }
        public PlayerViewModel Player { get; set; }
        public int BattingOrder { get; set; }
        public int PlateAppearances { get; set; }
        public int RunsScored { get; set; }
        public int Hits { get; set; }
        public int Doubles { get; set; }
        public int Triples { get; set; }
        public int HomeRuns { get; set; }
        public int Walks { get; set; }
        public int RunsBattedIn { get; set; }

        public ObjectState ObjectState { get; set;}        
    }
}