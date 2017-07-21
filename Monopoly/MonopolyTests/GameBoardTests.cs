using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;
using System;
using Monopoly.GameBoards;

namespace MonopolyTests
{
    [TestClass]
    public class GameBoardTests
    {
        private const Int32 GameBoardLength = 40;

        private Player player;
        private MonopolyGameBoard board;

        public GameBoardTests()
        {
            player = new Player("Richard");
            board = new MonopolyGameBoard(GameBoardLength);
        }

        [TestMethod]
        public void NewPlayerStartWithLocationZero()
        {
            Assert.AreEqual(0, player.Location);
        }
    }
}
