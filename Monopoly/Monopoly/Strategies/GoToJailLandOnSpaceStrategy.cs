using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Actions;

namespace Monopoly.Strategies
{
    public class GoToJailLandOnSpaceStrategy : ISpaceActionStrategy
    {
        private IEnumerable<IAction> actions;

        public GoToJailLandOnSpaceStrategy()
        {
            actions = new List<IAction>()
            {
                new TeleportAction()
            };
        }

        public IEnumerable<IAction> GetActions()
        {
            return actions;
        }
    }
}
