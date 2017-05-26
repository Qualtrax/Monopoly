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
            player = new Player();
            board = new GameBoard(player);
        }

        [TestMethod]
        public void NewPlayerStartWithLocationZero()
        {
            Assert.AreEqual(0, player.Location);
        }

        [TestMethod]
        public void MovingNewPlayerBySevenLandsOnSeven()
        {
            board.MovePlayer(7);

            Assert.AreEqual(7, player.Location);
        }

        [TestMethod]
        public void PlayerStartingOnThirtyNineAndMovesSixEndsOnFive()
        {
            player.Location = 39;
            board.MovePlayer(6);

            Assert.AreEqual(5, player.Location);
        }
    }
}
