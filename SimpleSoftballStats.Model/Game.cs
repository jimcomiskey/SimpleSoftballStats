using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleSoftballStats.Model
{
    public class Game : IObjectWithState
    {
        public int Id { get; set; }
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }
        public int? RunsScored { get; set; }
        public int? RunsAllowed { get; set; }
        public string Opponent { get; set; }
        public DateTime? GameTime { get; set; }

        public virtual ICollection<GameBoxScoreDetail> BoxScoreDetails { get; set; }

        public Game()
        {
            BoxScoreDetails = new List<GameBoxScoreDetail>();
        }

        public string Result
        {
            get
            {
                if (RunsScored == null || RunsAllowed == null)
                    return string.Empty;
                else if (RunsScored > RunsAllowed)
                    return "Win";
                else if (RunsScored < RunsAllowed)
                    return "Lose";
                else
                    return "Tie";
            }
        }

        public ObjectState ObjectState {get; set;}        
    }
}