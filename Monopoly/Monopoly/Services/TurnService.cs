using Monopoly.Factories;
using Monopoly.Spaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Strategies;
using Monopoly.Actions;
using Monopoly.GameBoards;

namespace Monopoly.Services
{
    public class TurnService : ITurnService
    {
        private IGameBoard gameBoard;
        private IEnterSpaceStrategyFactory enterSpaceStrategyFactory;
        private ILandOnSpaceStrategyFactory landOnSpaceStrategyFactory;
        private IActionExecutor actionExcecutor;

        public TurnService(IGameBoard gameBoard, IEnterSpaceStrategyFactory enterSpaceStrategyFactory,
            ILandOnSpaceStrategyFactory landOnSpaceStrategyFactory, IActionExecutor actionExcecutor)
        {
            this.gameBoard = gameBoard;
            this.enterSpaceStrategyFactory = enterSpaceStrategyFactory;
            this.landOnSpaceStrategyFactory = landOnSpaceStrategyFactory;
            this.actionExcecutor = actionExcecutor;
        }

        public void TakeTurn(Player player, Int32 spacesToMove)
        {
            var currentSpace = gameBoard.Spaces.ElementAt(player.Location);

            for (int i = 0; i < spacesToMove; i++)
            {
                player.Location = (1 + player.Location) % gameBoard.Spaces.Count();
                currentSpace = gameBoard.Spaces.ElementAt(player.Location);
                var enterSpaceStrategy = enterSpaceStrategyFactory.Create(currentSpace, player);
                actionExcecutor.Execute(enterSpaceStrategy, player);
            }

            var landOnSpaceStrategy = landOnSpaceStrategyFactory.Create(currentSpace, player);
            actionExcecutor.Execute(landOnSpaceStrategy, player);
        }
    }

}
