using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Spaces;
using Monopoly.Strategies;

namespace Monopoly.Factories
{
    public class LandOnSpaceStrategyFactory : ILandOnSpaceStrategyFactory
    {
        public ILandOnSpaceStrategy Create(ISpace space, Player player)
        {
            if (space is GenericSpace)
                return new GenericLandOnSpaceStrategy();
            else if (space is GoSpace)
                return new GoLandOnSpaceStrategy();
            else if (space is GoToJailSpace)
                return new GoToJailLandOnSpaceStrategy(player);
            else if (space is JustVisitingSpace)
                return new JustVisitingLandOnSpaceStrategy();

            else throw new InvalidOperationException("Space type not found");
        }
    }
}
