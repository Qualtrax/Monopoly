using Monopoly.Spaces;
using Monopoly.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Factories
{
    public interface ILandOnSpaceStrategyFactory
    {
        ILandOnSpaceStrategy Create(ISpace space, Player player);
    }
}
