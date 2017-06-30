using Monopoly.Factories;
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
        private IEnterSpaceStrategyFactory enterSpaceStrategyFactory;
        private ILandOnSpaceStrategyFactory landOnSpaceStrategyFactory;

        public MovementService(GameBoard gameBoard, IEnterSpaceStrategyFactory enterSpaceStrategyFactory,
            ILandOnSpaceStrategyFactory landOnSpaceStrategyFactory)
        {
            this.gameBoard = gameBoard;
            this.enterSpaceStrategyFactory = enterSpaceStrategyFactory;
            this.landOnSpaceStrategyFactory = landOnSpaceStrategyFactory;
        }

        public void MovePlayer(Player player, Int32 spacesToMove)
        {
            var currentSpace = gameBoard.Spaces[player.Location];

            for(int i = 0; i < spacesToMove; i++)
            {
                player.Location = (1 + player.Location) % gameBoard.NumberOfSpaces;
                currentSpace = gameBoard.Spaces[player.Location];
                var enterSpaceStrategy = enterSpaceStrategyFactory.Create(currentSpace, player);
                enterSpaceStrategy.Act();
            }

            var landOnSpaceStrategy = landOnSpaceStrategyFactory.Create(currentSpace, player);
            landOnSpaceStrategy.Act();
        }
    }
}
