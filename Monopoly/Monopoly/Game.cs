using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class Game
    {
        private IEnumerable<Player> players;

        public void Play(String[] playerNames, Int32 roundsToPlay)
        {
            VerifyNumberOfPlayers(playerNames.Count());
            CreatePlayers(playerNames);
            RandomizePlayerOrder();

            for (var i = 0; i < roundsToPlay; i++)
                PlayRound();
        }

        private void PlayRound()
        {
            foreach (var player in players)
                TakeTurn(player);
        }

        private void TakeTurn(Player player)
        {
            player.IncrementRoundsPlayed();
        }

        private void CreatePlayers(IEnumerable<String> playerNames)
        {
            players = playerNames.Select(s => new Player(s));
        }

        private void RandomizePlayerOrder()
        {
            players = players.OrderBy(p => Guid.NewGuid());
        }

        private void VerifyNumberOfPlayers(Int32 numberOfPlayers)
        {
            if (numberOfPlayers < 2)
                throw new ArgumentException("Cannot start game with less than " +
                    "2 players");
            else if(numberOfPlayers > 8)
                throw new ArgumentException("Cannot start game with greater than " +
                    "8 players");
        }

        public IEnumerable<String> GetPlayerNames()
        {
            return players.Select(p => p.Name);
        }

        public Int32 GetRoundsPlayed()
        {
            return 20;
        }

        public Int32 GetRoundsPlayed(String playerName)
        {
            return 0;
        }
    }
}
