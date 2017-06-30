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
        public IEnterSpaceStrategy Create(ISpace space, Player player)
        {
            if (space is GenericSpace)
                return new  GenericEnterSpaceStrategy();
            else if (space is GoSpace)
                return new GoEnterSpaceStrategy(player);
            else if (space is GoToJailSpace)
                return new GoToJailEnterSpaceStrategy();
            else if (space is JustVisitingSpace)
                return new JustVisitingEnterSpaceStrategy();

            else throw new InvalidOperationException("Space type not found");
        }
    }
}
