using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;

namespace MonopolyTests
{
    [TestClass]
    public class GameBoardTests
    {
        private Player player;
        private GameBoard board;

        public GameBoardTests()
        {
            player = new Player("Richard");
            board = new GameBoard();
        }

        [TestMethod]
        public void NewPlayerStartWithLocationZero()
        {
            Assert.AreEqual(0, player.Location);
        }


    }
}
