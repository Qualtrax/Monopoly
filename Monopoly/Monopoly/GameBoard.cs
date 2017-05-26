using System;

namespace Monopoly
{
    public class GameBoard
    {
        private const Int32 GameBoardLength = 40;

        private Player player;

        public GameBoard(Player player)
        {
            this.player = player;
        }

        public void MovePlayer(Int32 numberOfSpaces)
        {
            player.Location = (numberOfSpaces + player.Location) % GameBoardLength;
        }
    }
}
