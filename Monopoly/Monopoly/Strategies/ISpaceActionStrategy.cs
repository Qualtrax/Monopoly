using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Actions;

namespace Monopoly.Strategies
{
    public interface ISpaceActionStrategy
    {
        IEnumerable<IAction> GetActions();
    }
}
