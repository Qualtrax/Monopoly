using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Game
    {
        public static void Main(String[] playerNames)
        {
            VerifyNumberOfPlayers(playerNames);
            var players = CreatePlayers(playerNames);
            var gameBoard = new GameBoard(players);
        }

        private static IEnumerable<Player> CreatePlayers(IEnumerable<String> playerNames)
        {
            return playerNames.Select(s => new Player(s));
        }

        private static void VerifyNumberOfPlayers(String[] args)
        {
            var playersCount = args.Count();
            if (playersCount < 2)
                throw new ArgumentException("Cannot start game with "
                    + playersCount + " players");
        }
    }
}
