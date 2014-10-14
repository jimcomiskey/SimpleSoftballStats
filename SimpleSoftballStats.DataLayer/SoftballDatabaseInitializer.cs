using SimpleSoftballStats.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleSoftballStats.DataLayer
{
    public class SoftballDatabaseInitializer: DropCreateDatabaseIfModelChanges<SoftballContext>
    {
        protected override void Seed(SoftballContext context)
        {
            var teams = new List<Team>
            {
                new Team{TeamName="Warriors"}, 
                new Team{TeamName="Duck Snorts"}
            };

            foreach(var t in teams)
            {
                context.Teams.Add(t);
            }
            context.SaveChanges();

            var players = new List<Player>
            {
                new Player{FirstName="Jim", LastName="C."}, 
                new Player{FirstName="David", LastName="T."}, 
                new Player{FirstName="Kevin", LastName="R."},
                new Player{FirstName="Kevin", LastName="W."},
                new Player{FirstName="Julius", LastName="B."}, 
                new Player{FirstName="Bryan", LastName="G."}, 
                new Player{FirstName="Dan", LastName="K."}, 
                new Player{FirstName="Teri", LastName="K."}, 
                new Player{FirstName="Erin", LastName="K."}, 
                new Player{FirstName="Jenny", LastName="M."}, 
                new Player{FirstName="Heather", LastName="J."}, 
                new Player{FirstName="Melissa", LastName="P."}                    
            };

            foreach (var p in players)
            {
                context.Players.Add(p);
            }

            var rosterEntries = new List<RosterEntry>
            {
                new RosterEntry{PlayerId = 1, TeamId = 1}, 
                new RosterEntry{PlayerId = 2, TeamId = 1}, 
                new RosterEntry{PlayerId = 3, TeamId = 1}, 
                new RosterEntry{PlayerId = 4, TeamId = 1}, 
                new RosterEntry{PlayerId = 5, TeamId = 1}, 
                new RosterEntry{PlayerId = 6, TeamId = 1}, 
                new RosterEntry{PlayerId = 7, TeamId = 1}, 
                new RosterEntry{PlayerId = 8, TeamId = 1}, 
                new RosterEntry{PlayerId = 9, TeamId = 1}, 
                new RosterEntry{PlayerId = 10, TeamId = 1}, 
                new RosterEntry{PlayerId = 1, TeamId = 2}, 
                new RosterEntry{PlayerId = 2, TeamId = 2}, 
                new RosterEntry{PlayerId = 3, TeamId = 2}, 
                new RosterEntry{PlayerId = 4, TeamId = 2}, 
                new RosterEntry{PlayerId = 5, TeamId = 2}, 
                new RosterEntry{PlayerId = 9, TeamId = 2}, 
                new RosterEntry{PlayerId = 10, TeamId = 2}, 
                new RosterEntry{PlayerId = 11, TeamId = 2}, 
                new RosterEntry{PlayerId = 12, TeamId = 2}
            };

            foreach (var re in rosterEntries)
            {
                context.RosterEntries.Add(re);
            }

            context.SaveChanges();

            var games = new List<Game>
            {
                new Game{TeamId = 1, Opponent = "Cavemen", RunsScored = 11, RunsAllowed = 5}, 
                new Game{TeamId = 1, Opponent = "No Mercy", RunsScored = 20, RunsAllowed = 19}, 
                new Game{TeamId = 1, Opponent = "Smackin Pitches", RunsScored = 11, RunsAllowed = 12}, 
                new Game{TeamId = 1, Opponent = "Lugnuts", RunsScored = 23, RunsAllowed = 2}, 
                new Game{TeamId = 1, Opponent = "Ninjas", RunsScored = 5, RunsAllowed = 20}, 
                new Game{TeamId = 1, Opponent = "Crush", RunsScored = 16, RunsAllowed = 8}
            };

            foreach (var g in games)
                context.Games.Add(g);

            context.SaveChanges();

            games[0].BoxScoreDetails = new List<GameBoxScoreDetail>
            {
                //                     G Pl BO PA  R  H 2B 3B HR BB RBI
                new GameBoxScoreDetail(1, 1, 1, 4, 2, 2, 0, 0, 0, 0, 2), 
                new GameBoxScoreDetail(1, 2, 2, 4, 1, 2, 1, 0, 0, 1, 1), 
                new GameBoxScoreDetail(1, 3, 3, 4, 3, 3, 1, 0, 1, 0, 4), 
                new GameBoxScoreDetail(1, 4, 4, 4, 0, 1, 1, 0, 0, 1, 2), 
                new GameBoxScoreDetail(1, 5, 5, 3, 0, 1, 0, 0, 0, 0, 0), 
                new GameBoxScoreDetail(1, 6, 6, 3, 1, 2, 0, 0, 1, 0, 0), 
                new GameBoxScoreDetail(1, 7, 7, 3, 2, 3, 1, 1, 0, 0, 3), 
                new GameBoxScoreDetail(1, 8, 8, 3, 0, 1, 0, 0, 0, 0, 0), 
                new GameBoxScoreDetail(1, 9, 9, 3, 3, 2, 0, 0, 0, 1, 0), 
                new GameBoxScoreDetail(1, 10, 10, 3, 0, 0, 0, 0, 0, 1, 0)

            };

            games[1].BoxScoreDetails = new List<GameBoxScoreDetail>
            {
                //                     G Pl BO PA  R  H 2B 3B HR BB RBI
                new GameBoxScoreDetail(2, 1, 1, 5, 1, 1, 0, 0, 0, 0, 0), 
                new GameBoxScoreDetail(2, 2, 2, 5, 1, 0, 0, 0, 0, 1, 0), 
                new GameBoxScoreDetail(2, 3, 3, 5, 1, 1, 0, 0, 1, 0, 0), 
                new GameBoxScoreDetail(2, 4, 4, 5, 2, 2, 1, 0, 1, 0, 4), 
                new GameBoxScoreDetail(2, 5, 5, 5, 0, 1, 0, 0, 0, 0, 1), 
                new GameBoxScoreDetail(2, 6, 6, 4, 0, 1, 0, 0, 0, 0, 0), 
                new GameBoxScoreDetail(2, 7, 7, 4, 1, 3, 1, 1, 0, 0, 3), 
                new GameBoxScoreDetail(2, 8, 8, 4, 0, 1, 0, 0, 0, 0, 1), 
                new GameBoxScoreDetail(2, 9, 9, 4, 0, 2, 0, 0, 0, 1, 0), 
                new GameBoxScoreDetail(2, 10, 10, 4, 0, 0, 0, 0, 0, 1, 0)

            };

            context.SaveChanges();
            

            base.Seed(context);
        }
    }
}