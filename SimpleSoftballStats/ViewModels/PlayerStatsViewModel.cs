using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleSoftballStats.ViewModels
{
    public class PlayerStatsViewModel
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PlayerFullName
        {
            get
            {
                return String.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public int G { get; set; }
        public int PA { get; set; }
        public int AB 
        {
            get
            {
                return PA - BB;
            }
        }
        
        public int Runs { get; set; }
        public int Hits { get; set; }
        public int Doubles { get; set; }
        public int Triples { get; set; }
        public int HR { get; set; }
        public int BB { get; set; }
        public int RBI { get; set; }
        public float BattingAvg { get; set; }
        public float OnBasePct { get; set; }
        public float SluggingPct { get; set; }
    }
}