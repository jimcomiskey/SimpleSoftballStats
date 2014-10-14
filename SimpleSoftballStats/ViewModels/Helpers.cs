using SimpleSoftballStats.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleSoftballStats.ViewModels
{
    public static class Helpers
    {
        public static string GetMessageToClient(ObjectState objectState, string teamName)
        {
            string messageToClient = string.Empty;
            switch (objectState)
            {
                case ObjectState.Added:
                    messageToClient = "Team has been added";
                    break;
                case ObjectState.Modified:
                    messageToClient = "Team has been modified.";
                    break;
            }

            return messageToClient;
        }
        
        public static TeamViewModel CreateTeamViewModelFromTeamAndPlayers(Team team, IEnumerable<Player> players)
        {
            var teamViewModel = new TeamViewModel();

            if (team != null)
            {
                teamViewModel.TeamId = team.TeamId;
                teamViewModel.TeamName = team.TeamName;
                teamViewModel.ObjectState = ObjectState.Unchanged;

                foreach(var rosterEntry in team.RosterEntries)
                {
                    RosterEntryViewModel rosterEntryViewModel = new RosterEntryViewModel();
                    rosterEntryViewModel.TeamId = rosterEntry.TeamId;
                    rosterEntryViewModel.PlayerId = rosterEntry.PlayerId;
                
                    PlayerViewModel rosterEntryPlayer = new PlayerViewModel();
                    rosterEntryPlayer.FirstName = rosterEntry.Player.FirstName;
                    rosterEntryPlayer.LastName = rosterEntry.Player.LastName;
                
                    rosterEntryViewModel.Player = rosterEntryPlayer;
                    rosterEntryViewModel.ObjectState = ObjectState.Unchanged;

                    teamViewModel.RosterEntries.Add(rosterEntryViewModel);
                
                }
            }

            foreach(var player in players)
            {
                var playerViewModel = CreatePlayerViewModelFromPlayer(player);
                
                teamViewModel.AvailablePlayers.Add(playerViewModel);
            }

            return teamViewModel;
        }
        public static Team CreateTeamFromTeamViewModel(TeamViewModel teamViewModel)
        {
            var team = new Team();
            team.TeamId = teamViewModel.TeamId;
            team.TeamName = teamViewModel.TeamName;
            team.ObjectState = teamViewModel.ObjectState;

            foreach (RosterEntryViewModel rosterEntryViewModel in teamViewModel.RosterEntries)
            {
                RosterEntry rosterEntry = new RosterEntry();
                rosterEntry.PlayerId = rosterEntryViewModel.PlayerId;
                rosterEntry.Player = new Player() { PlayerId = rosterEntryViewModel.PlayerId, FirstName = rosterEntryViewModel.Player.FirstName, LastName = rosterEntryViewModel.Player.LastName };
                rosterEntry.TeamId = rosterEntryViewModel.TeamId;
                rosterEntry.ObjectState = rosterEntryViewModel.ObjectState;

                team.RosterEntries.Add(rosterEntry);
            }

            return team;
        }

        public static PlayerViewModel CreatePlayerViewModelFromPlayer(Player player)
        {
            PlayerViewModel playerViewModel = new PlayerViewModel();
            playerViewModel.PlayerId = player.PlayerId;
            playerViewModel.FirstName = player.FirstName;
            playerViewModel.LastName = player.LastName;
            playerViewModel.ObjectState = ObjectState.Unchanged;

            return playerViewModel;
            
        }

        public static GameViewModel CreateGameViewModelFromGame(Game game)
        {
            GameViewModel gameViewModel = new GameViewModel();
            gameViewModel.Id = game.Id;
            gameViewModel.TeamId = game.TeamId;
            //gameViewModel.Team = game.Team;
            gameViewModel.Opponent = game.Opponent;
            gameViewModel.RunsScored = game.RunsScored;
            gameViewModel.RunsAllowed = game.RunsAllowed;
            gameViewModel.GameTime = game.GameTime;

            foreach(var gameBoxScoreDetail in game.BoxScoreDetails.OrderBy(b => b.BattingOrder))
            {
                GameBoxScoreDetailViewModel vm = CreateGameBoxScoreDetailViewModelFromModel(gameBoxScoreDetail);
                gameViewModel.BoxScoreDetails.Add(vm);

            }

            gameViewModel.ObjectState = ObjectState.Unchanged;

            return gameViewModel;
            
        }
        public static Game CreateGameFromGameViewModel (GameViewModel gameViewModel)
        {
            Game game = new Game();
            game.Id = gameViewModel.Id;
            game.TeamId = gameViewModel.TeamId;
            game.Team = gameViewModel.Team;
            game.Opponent = gameViewModel.Opponent;
            game.RunsScored = gameViewModel.RunsScored;
            game.RunsAllowed = gameViewModel.RunsAllowed;
            game.GameTime = gameViewModel.GameTime;

            foreach(var boxScoreDetail in gameViewModel.BoxScoreDetails)
            {
                GameBoxScoreDetail gameBoxScoreDetail = CreateGameBoxScoreDetailModelFromViewModel(boxScoreDetail);
                game.BoxScoreDetails.Add(gameBoxScoreDetail);
            }

            game.ObjectState = gameViewModel.ObjectState;

            return game;
        }

        public static GameBoxScoreDetailViewModel CreateGameBoxScoreDetailViewModelFromModel(GameBoxScoreDetail gameBoxScoreDetail)
        {
            var boxScoreDetailVM = new GameBoxScoreDetailViewModel();
            boxScoreDetailVM.PlayerId = gameBoxScoreDetail.PlayerId;
            boxScoreDetailVM.Player = ViewModels.Helpers.CreatePlayerViewModelFromPlayer(gameBoxScoreDetail.Player);
            boxScoreDetailVM.GameId = gameBoxScoreDetail.GameId;
            boxScoreDetailVM.BattingOrder = gameBoxScoreDetail.BattingOrder;
            boxScoreDetailVM.PlateAppearances = gameBoxScoreDetail.PlateAppearances;
            boxScoreDetailVM.RunsScored = gameBoxScoreDetail.RunsScored;
            boxScoreDetailVM.Hits = gameBoxScoreDetail.Hits;                        
            boxScoreDetailVM.Doubles = gameBoxScoreDetail.Doubles;
            boxScoreDetailVM.Triples = gameBoxScoreDetail.Triples;
            boxScoreDetailVM.HomeRuns = gameBoxScoreDetail.HomeRuns;
            boxScoreDetailVM.Walks = gameBoxScoreDetail.Walks;
            boxScoreDetailVM.RunsBattedIn = gameBoxScoreDetail.RunsBattedIn;

            boxScoreDetailVM.ObjectState = ObjectState.Unchanged;

            return boxScoreDetailVM;
        }
        public static GameBoxScoreDetail CreateGameBoxScoreDetailModelFromViewModel(GameBoxScoreDetailViewModel gameBoxScoreDetailVM)
        {
            var gameBoxScoreDetail = new GameBoxScoreDetail();
            gameBoxScoreDetail.GameId = gameBoxScoreDetailVM.GameId;
            
            gameBoxScoreDetail.PlayerId = gameBoxScoreDetailVM.PlayerId;            
            gameBoxScoreDetail.BattingOrder = gameBoxScoreDetailVM.BattingOrder;
            gameBoxScoreDetail.PlateAppearances = gameBoxScoreDetailVM.PlateAppearances;
            gameBoxScoreDetail.RunsScored = gameBoxScoreDetailVM.RunsScored;
            gameBoxScoreDetail.Hits = gameBoxScoreDetailVM.Hits;
            gameBoxScoreDetail.Doubles = gameBoxScoreDetailVM.Doubles;
            gameBoxScoreDetail.Triples = gameBoxScoreDetailVM.Triples;
            gameBoxScoreDetail.HomeRuns = gameBoxScoreDetailVM.HomeRuns;
            gameBoxScoreDetail.Walks = gameBoxScoreDetailVM.Walks;
            gameBoxScoreDetail.RunsBattedIn = gameBoxScoreDetailVM.RunsBattedIn;

            gameBoxScoreDetail.ObjectState = gameBoxScoreDetailVM.ObjectState;

            return gameBoxScoreDetail;
        }
    }
}