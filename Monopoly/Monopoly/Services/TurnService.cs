using Monopoly.Factories;
using Monopoly.Spaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Strategies;
using Monopoly.Actions;

namespace Monopoly.Services
{
    public class TurnService : ITurnService
    {
        private GameBoard gameBoard;
        private IEnterSpaceStrategyFactory enterSpaceStrategyFactory;
        private ILandOnSpaceStrategyFactory landOnSpaceStrategyFactory;

        public TurnService(GameBoard gameBoard, IEnterSpaceStrategyFactory enterSpaceStrategyFactory,
            ILandOnSpaceStrategyFactory landOnSpaceStrategyFactory)
        {
            this.gameBoard = gameBoard;
            this.enterSpaceStrategyFactory = enterSpaceStrategyFactory;
            this.landOnSpaceStrategyFactory = landOnSpaceStrategyFactory;
        }

        public void TakeTurn(Player player, Int32 spacesToMove)
        {
            var currentSpace = gameBoard.Spaces[player.Location];

            for (int i = 0; i < spacesToMove; i++)
            {
                player.Location = (1 + player.Location) % gameBoard.NumberOfSpaces;
                currentSpace = gameBoard.Spaces[player.Location];
                var enterSpaceStrategy = enterSpaceStrategyFactory.Create(currentSpace, player);
                Execute(enterSpaceStrategy, player);
            }

            var landOnSpaceStrategy = landOnSpaceStrategyFactory.Create(currentSpace, player);
            Execute(landOnSpaceStrategy, player);
        }

        private void Execute(ISpaceActionStrategy strategy, Player player)
        {
            foreach (var action in strategy.GetActions())
            {
                var actionType = action.GetType();

                if (actionType == typeof(PayAction))
                {
                    var payAction = action as PayAction;
                    player.AddFunds(payAction.Amount);
                }
                else if (actionType == typeof(TeleportAction))
                {
                    var teleportAction = action as TeleportAction;
                    player.Location = MonopolyConstants.JailLocation;
                }
            }
        }
    }

}
