using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;

namespace MonopolyTests
{
    [TestClass]
    public class GameDriverTests
    {
        [TestMethod]
        public void CreatingGameWithOnePlayerFails()
        {
            try
            {
                var players = new[] { "Tim" };
                Game.Main(players);
                Assert.Fail();
            }
            catch (Exception caught)
            {
                Assert.IsInstanceOfType(caught, typeof(ArgumentException));
                var expectedMessage = "Cannot start game with 1 players";
                Assert.AreEqual(expectedMessage, caught.Message);
            }
        }
    }
}
