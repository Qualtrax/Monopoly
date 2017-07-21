using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Spaces;

namespace Monopoly.GameBoards
{
    public interface IGameBoard
    {
        IEnumerable<ISpace> Spaces { get; }
    }
}
