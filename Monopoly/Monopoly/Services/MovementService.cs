using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Services
{
    public class MovementService : IMovementService
    {
        private GameBoard gameBoard;

        public MovementService(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
        }

        public void MovePlayer(Player player, Int32 spacesToMove)
        {
            player.Location = (spacesToMove + player.Location) % gameBoard.Length;
        }
    }
}
