using Monopoly.Spaces;
using Monopoly.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Factories
{
    public class EnterSpaceStrategyFactory : IEnterSpaceStrategyFactory
    {
        public ISpaceActionStrategy Create(ISpace space, Player player)
        {
            if (space is GoSpace)
                return new GoEnterSpaceStrategy();
            else
                return new EmptySpaceActionStrategy();
        }
    }
}
