using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class GameBoard
    {
        public Int32 NumberOfSpaces { get; set; }

        public GameBoard(Int32 numberOfSpaces)
        {
            NumberOfSpaces = numberOfSpaces;
        }
    }
}
