using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class GameBoard
    {
        private const Int32 GameBoardLength = 40;

        public IEnumerable<Player> Players { get; private set; }

        public GameBoard(IEnumerable<Player> players)
        {
            Players = players;
        }

        public void MovePlayer(Int32 numberOfSpaces)
        {
            var firstPlayer = Players.FirstOrDefault();
            firstPlayer.Location = (numberOfSpaces + firstPlayer.Location) % GameBoardLength;
        }
    }
}
