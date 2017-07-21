using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Actions;
using Monopoly.Strategies;

namespace Monopoly.Services
{
    public class ActionExecutor
    {
        private void Execute(ISpaceActionStrategy strategy, Player player)
        {
            foreach (var action in strategy.GetActions())
            {
                var actionType = action.GetType();

                if (actionType == typeof(PayAction))
                {
                    var payAction = action as PayAction;
                    player.Balance += payAction.Amount;
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
