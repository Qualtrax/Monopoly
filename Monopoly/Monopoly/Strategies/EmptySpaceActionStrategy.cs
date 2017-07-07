using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Actions;

namespace Monopoly.Strategies
{
    public class EmptySpaceActionStrategy : ISpaceActionStrategy
    {
        public IEnumerable<IAction> GetActions()
        {
            return Enumerable.Empty<IAction>();
        }
    }
}
