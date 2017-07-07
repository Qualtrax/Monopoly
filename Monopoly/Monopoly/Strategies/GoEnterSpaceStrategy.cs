using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Actions;

namespace Monopoly.Strategies
{
    public class GoEnterSpaceStrategy : ISpaceActionStrategy
    {
        private IEnumerable<IAction> actions;

        public GoEnterSpaceStrategy()
        {
            actions = new List<IAction>()
            {
                new PayAction(MonopolyConstants.PassGoAmount)
            };
        }

        public IEnumerable<IAction> GetActions()
        {
            return actions;
        }
    }
}
