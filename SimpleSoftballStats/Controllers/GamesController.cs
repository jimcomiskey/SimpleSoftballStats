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

namespace SimpleSoftballStats.Controllers
{
    public class GamesController : Controller
    {
        private SoftballContext _softballContext;

        public GamesController()
        {
            _softballContext = new SoftballContext();
        }

        // GET: Games
        public ActionResult Index()
        {
            var games = _softballContext.Games.Include(g => g.Team);

            return View(games.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = _softballContext.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            var gameViewModel = ViewModels.Helpers.CreateGameViewModelFromGame(game);
            gameViewModel.MessageToClient = "Viewing Game Details";

            return View(gameViewModel);
        }

        public ActionResult Create()
        {
            GameViewModel gameViewModel = new GameViewModel();
            foreach (Team t in _softballContext.Teams)
            {
                gameViewModel.AvailableTeams.Add(new TeamViewModel() { TeamId = t.TeamId, TeamName = t.TeamName });
            }
            foreach (Player p in _softballContext.Players)
            {
                gameViewModel.AvailablePlayers.Add(ViewModels.Helpers.CreatePlayerViewModelFromPlayer(p));
            }
            gameViewModel.ObjectState = ObjectState.Added;

            return View(gameViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = _softballContext.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            var gameViewModel = ViewModels.Helpers.CreateGameViewModelFromGame(game);
            gameViewModel.MessageToClient = "Edit Game Details";

            foreach(Team t in _softballContext.Teams)
            {
                gameViewModel.AvailableTeams.Add(new TeamViewModel() { TeamId = t.TeamId, TeamName = t.TeamName });
            }            
            foreach(Player p in _softballContext.Players)
            {
                gameViewModel.AvailablePlayers.Add(ViewModels.Helpers.CreatePlayerViewModelFromPlayer(p));
            }
            
            return View(gameViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = _softballContext.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            var gameViewModel = ViewModels.Helpers.CreateGameViewModelFromGame(game);
            gameViewModel.MessageToClient = String.Format("You are about to delete game: {0} vs {1} on {2}", game.Team.TeamName, game.Opponent, game.GameTime);
            gameViewModel.ObjectState = ObjectState.Deleted;

            return View(gameViewModel);
        }

        public JsonResult Save(GameViewModel gameViewModel)
        {
            Game game = ViewModels.Helpers.CreateGameFromGameViewModel(gameViewModel);

            try
            {
                _softballContext.Games.Attach(game);
            }
            catch (Exception ex)
            {
                string sMessage = ex.ToString();
            }

            if (gameViewModel.ObjectState == ObjectState.Deleted)
            {
                foreach (GameBoxScoreDetailViewModel boxScoreDetailVM in gameViewModel.BoxScoreDetails)
                {
                    GameBoxScoreDetail boxScoreDetail  = _softballContext.GameBoxScoreDetails.Where(r => r.PlayerId == boxScoreDetailVM.PlayerId && r.GameId == boxScoreDetailVM.GameId).FirstOrDefault();
                    if (boxScoreDetail != null)
                        boxScoreDetail.ObjectState = ObjectState.Deleted;
                }
            }
            else
            {
                foreach (int playerId in gameViewModel.BoxScoreDetailsToDelete)
                {
                    GameBoxScoreDetail boxScoreDetail = _softballContext.GameBoxScoreDetails.Where(r => r.PlayerId == playerId && r.GameId == game.Id).FirstOrDefault();
                    if (boxScoreDetail != null)
                    {
                        GameBoxScoreDetailViewModel boxScoreDetailVM = gameViewModel.BoxScoreDetails.Where(r => r.PlayerId == playerId && r.GameId == boxScoreDetail.GameId && r.ObjectState == ObjectState.Added).FirstOrDefault();
                        if (boxScoreDetailVM == null)
                            boxScoreDetail.ObjectState = ObjectState.Deleted;
                        else
                            // if player from box score was removed and then re-added, treat it as though it were being 
                            // modified instead of deleted and re-added.
                            boxScoreDetail.ObjectState = ObjectState.Modified;
                    }
                }
            }

            _softballContext.ApplyStateChanges();
            _softballContext.SaveChanges();

            if (game.ObjectState == ObjectState.Deleted)
                return Json(new { newLocation = "/Games/Index/" });

            string messageToClient = ViewModels.Helpers.GetMessageToClient(gameViewModel.ObjectState, String.Format("{0} {1}", gameViewModel.Opponent, gameViewModel.GameTime));

            gameViewModel = ViewModels.Helpers.CreateGameViewModelFromGame(game);
            gameViewModel.MessageToClient = messageToClient;

            foreach (Team t in _softballContext.Teams)
            {
                gameViewModel.AvailableTeams.Add(new TeamViewModel() { TeamId = t.TeamId, TeamName = t.TeamName });
            }            

            return Json(new { gameViewModel });
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
