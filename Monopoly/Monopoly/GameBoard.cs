using Monopoly.Spaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class GameBoard
    {
        public Int32 NumberOfSpaces { get; set; }
        public ISpace[] Spaces { get; private set; }

        public GameBoard(Int32 numberOfSpaces)
        {
            NumberOfSpaces = numberOfSpaces;
            SetUpSpaces();
        }

        private void SetUpSpaces()
        {
            Spaces = new ISpace[NumberOfSpaces];
            Spaces[0] = new GoSpace();
            for (int i = 1; i < NumberOfSpaces; i++)
            {
                Spaces[i] = new GenericSpace();
            }
            Spaces[MonopolyConstants.GoToJailLocation] = new GoToJailSpace();
            Spaces[MonopolyConstants.JailLocation] = new JustVisitingSpace();
        }
    }
}
