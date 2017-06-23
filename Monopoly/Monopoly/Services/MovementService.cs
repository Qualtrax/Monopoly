using Monopoly.Spaces;
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
            for(int i = 0; i < spacesToMove; i++)
            {
                player.Location = (1 + player.Location) % gameBoard.NumberOfSpaces;
                if (gameBoard.Spaces[player.Location] is GoSpace)
                    player.AddFunds(200);
            }
            
        }
    }
}
