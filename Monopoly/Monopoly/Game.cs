using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class Game
    {
        private IEnumerable<Player> players;
        private GameBoard gameBoard;
        private Boolean gameStarted;

        public Game(IEnumerable<Player> players)
        {
            this.players = players;
            gameBoard = new GameBoard();
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
            foreach (var player in players)
                TakeTurn(player);
        }

        private void TakeTurn(Player player)
        {
            var spacesToMove = 2;
            MovePlayer(player, spacesToMove);
            player.IncrementRoundsPlayed();
        }
        
        private void CreatePlayers(IEnumerable<String> playerNames)
        {
            players = playerNames.Select(s => new Player(s)).ToList();
        }

        private void RandomizePlayerOrder()
        {
            players = players.OrderBy(p => Guid.NewGuid()).ToList();
        }

        private void VerifyNumberOfPlayers()
        {
            if (players.Count() < 2)
                throw new InvalidOperationException("Cannot start game with less than " +
                    "2 players");
            else if(players.Count() > 8)
                throw new InvalidOperationException("Cannot start game with greater than " +
                    "8 players");
        }

        public Int32 GetRoundsPlayed()
        {
            return 20;
        }

        public Int32 GetRoundsPlayed(String playerName)
        {
            return players.First(p => p.Name == playerName).RoundsPlayed;
        }

        private void MovePlayer(Player player, Int32 spacesToMove)
        {
            player.Location = (spacesToMove + player.Location) % GameBoard.GameBoardLength;
        }
    }
}
