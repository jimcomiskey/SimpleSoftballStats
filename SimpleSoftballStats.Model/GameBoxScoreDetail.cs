using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleSoftballStats.Model
{
    public class GameBoxScoreDetail : IObjectWithState
    {
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public int BattingOrder { get; set; }
        public int PlateAppearances { get; set; }
        public int RunsScored {get; set;}
        public int Hits { get; set; }
        public int Doubles { get; set; }
        public int Triples { get; set; }
        public int HomeRuns { get; set; }
        public int Walks { get; set; }                
        public int RunsBattedIn { get; set; }
        public GameBoxScoreDetail()
        {
            // do nothing
        }
        public GameBoxScoreDetail
            (int GameId, int PlayerId, int BattingOrder, int PlateAppearances, 
            int RunsScored, int Hits, int Doubles, int Triples, int HomeRuns, 
            int Walks, int RunsBattedIn)
        {
            this.GameId = GameId;
            this.PlayerId = PlayerId;
            this.BattingOrder = BattingOrder;
            this.PlateAppearances = PlateAppearances;
            this.RunsScored = RunsScored;
            this.Hits = Hits;
            this.Doubles = Doubles;
            this.Triples = Triples;
            this.HomeRuns = HomeRuns;
            this.Walks = Walks;
            this.RunsBattedIn = RunsBattedIn;
        }

        public ObjectState ObjectState {get; set;}
        
    }
}