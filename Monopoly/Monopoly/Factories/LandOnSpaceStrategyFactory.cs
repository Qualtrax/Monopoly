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
        public ISpaceActionStrategy Create(ISpace space, Player player)
        {
            if (space is GoToJailSpace)
                return new GoToJailLandOnSpaceStrategy();
            else if (space is IncomeTaxSpace)
                return new IncomeTaxLandOnSpaceStrategy();
            else
                return new EmptySpaceActionStrategy();
        }
    }
}
