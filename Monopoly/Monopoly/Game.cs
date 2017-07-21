using Monopoly.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class Game
    {
        public IEnumerable<Player> Players { get; private set; }

        private ITurnService turnService;
        private Boolean gameStarted;

        public Game(IEnumerable<Player> players, ITurnService movementService)
        {
            this.Players = players;
            this.turnService = movementService;
            gameStarted = false;
        }

        public void Play(Int32 roundsToPlay)
        {
            if (!gameStarted)
                SetUpGame();
            
            for (var i = 0; i < roundsToPlay; i++)
                PlayRound();
        }

        private void SetUpGame()
        {
            VerifyNumberOfPlayers();
            RandomizePlayerOrder();
            gameStarted = true;
        }

        private void PlayRound()
        {
            foreach (var player in Players)
                TakeTurn(player);
        }

        private void TakeTurn(Player player)
        {
            var spacesToMove = 1;
            turnService.TakeTurn(player, spacesToMove);
            player.RoundsPlayed++;
        }

        private void RandomizePlayerOrder()
        {
            Players = Players.OrderBy(p => Guid.NewGuid()).ToList();
        }

        private void VerifyNumberOfPlayers()
        {
            if (Players.Count() < 2)
                throw new InvalidOperationException("Cannot start game with less than " +
                    "2 players");
            else if(Players.Count() > 8)
                throw new InvalidOperationException("Cannot start game with greater than " +
                    "8 players");
        }

        public Int32 GetRoundsPlayed()
        {
            return 20;
        }

        public Int32 GetRoundsPlayed(String playerName)
        {
            return Players.First(p => p.Name == playerName).RoundsPlayed;
        }
    }
}
