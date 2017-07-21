using Monopoly.Spaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly.GameBoards
{
    public class MonopolyGameBoard : IGameBoard
    {
        public Int32 NumberOfSpaces { get; set; }
        public IEnumerable<ISpace> Spaces { get; private set; }

        public MonopolyGameBoard(Int32 numberOfSpaces)
        {
            NumberOfSpaces = numberOfSpaces;
            SetUpSpaces();
        }

        private void SetUpSpaces()
        {
            var spaces = new List<ISpace>();
            spaces.Add(new GoSpace());

            for (var i = 1; i < NumberOfSpaces; i++)
                spaces.Add(new GenericSpace());

            spaces[MonopolyConstants.GoToJailLocation] = new GoToJailSpace();
            spaces[MonopolyConstants.JailLocation] = new JustVisitingSpace();
            Spaces = spaces;
        }
    }
}
