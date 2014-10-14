using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleSoftballStats.Model;
using SimpleSoftballStats.DataLayer;
using SimpleSoftballStats.ViewModels;
using System.Web.Helpers;

namespace SimpleSoftballStats.Controllers
{
    public class TeamsController : Controller
    {
        private SoftballContext _softballContext;

        public TeamsController()
        {
            _softballContext = new SoftballContext();
        }

        public ActionResult Index()
        {
            return View(_softballContext.Teams.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = _softballContext.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }

            var teamViewModel = ViewModels.Helpers.CreateTeamViewModelFromTeamAndPlayers(team, _softballContext.Players);
            teamViewModel.MessageToClient = "Viewing Team Details";

            teamViewModel.PlayerStats = (from p in _softballContext.GameBoxScoreDetails
                              where p.Game.TeamId == id                              
                              join pl in _softballContext.Players on p.PlayerId equals pl.PlayerId
                              group p by new { p.PlayerId, p.Player.FirstName, p.Player.LastName } into g                              
                              orderby g.Count() descending
                              select new PlayerStatsViewModel()
                              {
                                  PlayerId = g.Key.PlayerId,                                  
                                  FirstName =  g.Key.FirstName, 
                                  LastName = g.Key.LastName, 
                                  G = g.Count(), 
                                  PA = g.Sum(p => p.PlateAppearances),
                                  Runs = g.Sum(p => p.RunsScored), 
                                  Hits = g.Sum(p => p.Hits),
                                  Doubles = g.Sum(p => p.Doubles),
                                  Triples = g.Sum(p => p.Triples),
                                  HR = g.Sum(p => p.HomeRuns), 
                                  BB = g.Sum(p => p.Walks), 
                                  RBI = g.Sum(p => p.RunsBattedIn), 
                                  BattingAvg =  (float)Math.Round((double)g.Sum(p => p.Hits) / (double)g.Sum(p => p.PlateAppearances - p.Walks), 3), 
                                  OnBasePct = (float)Math.Round((float)g.Sum(p => p.Hits + p.Walks) / (float)g.Sum(p => p.PlateAppearances), 3), 
                                  SluggingPct = 1
                              }).ToList();

            return View(teamViewModel);
        }

        public ActionResult Create()
        {
            var teamViewModel = ViewModels.Helpers.CreateTeamViewModelFromTeamAndPlayers(null, _softballContext.Players);
            teamViewModel.ObjectState = ObjectState.Added;
            
            return View(teamViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = _softballContext.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            var teamViewModel = ViewModels.Helpers.CreateTeamViewModelFromTeamAndPlayers(team, _softballContext.Players);
            teamViewModel.MessageToClient = String.Format("The original value of Team Name is {0}", team.TeamName);

            return View(teamViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = _softballContext.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }

            var teamViewModel = ViewModels.Helpers.CreateTeamViewModelFromTeamAndPlayers(team, _softballContext.Players);
            teamViewModel.MessageToClient = String.Format("You are about to delete team: {0}", team.TeamName);            
            teamViewModel.ObjectState = ObjectState.Deleted;

            return View(teamViewModel);
        }

        public JsonResult Save(TeamViewModel teamViewModel)
        {
            
            Team team = ViewModels.Helpers.CreateTeamFromTeamViewModel(teamViewModel);            

            _softballContext.Teams.Attach(team);

            if (teamViewModel.ObjectState == ObjectState.Deleted)
            {
                foreach (RosterEntryViewModel revm in teamViewModel.RosterEntries)
                {
                    RosterEntry re = _softballContext.RosterEntries.Where(r => r.PlayerId == revm.PlayerId && r.TeamId == revm.TeamId).FirstOrDefault();
                    if (re != null)
                        re.ObjectState = ObjectState.Deleted;
                }
            }
            else
            {
                foreach (int playerId in teamViewModel.RosterEntriesToDelete)
                {
                    RosterEntry re = _softballContext.RosterEntries.Where(r => r.PlayerId == playerId && r.TeamId == team.TeamId).FirstOrDefault();
                    if (re != null)
                    {                        
                        RosterEntryViewModel rev = teamViewModel.RosterEntries.Where(r => r.PlayerId == playerId && r.TeamId == team.TeamId && r.ObjectState == ObjectState.Added).FirstOrDefault();
                        if (rev == null)
                            re.ObjectState = ObjectState.Deleted;
                        else
                            // if roster entry player is in view model and showing as "new", 
                            // this means the player was re-added after being deleted. 
                            // cancel any changes to it.
                            re.ObjectState = ObjectState.Unchanged;
                    }
                }
            }

            _softballContext.ApplyStateChanges();
            _softballContext.SaveChanges();

            if (team.ObjectState == ObjectState.Deleted)
                return Json(new { newLocation = "/Teams/Index/" });

            string messageToClient = ViewModels.Helpers.GetMessageToClient(teamViewModel.ObjectState, teamViewModel.TeamName);

            teamViewModel = ViewModels.Helpers.CreateTeamViewModelFromTeamAndPlayers(team, _softballContext.Players);
            teamViewModel.MessageToClient = messageToClient;

            return Json(new { teamViewModel });

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _softballContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
