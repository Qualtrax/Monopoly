using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;

namespace MonopolyTests
{
    [TestClass]
    public class PlayerTests
    {
        Player player;

        public PlayerTests()
        {
            player = new Player("Tim");
        }

        [TestMethod]
        public void CreatePlayerWithNameTim()
        {
            Assert.AreEqual("Tim", player.Name);
        }

        [TestMethod]
        public void CreatePlayerInitialLocationAtZero()
        {
            Assert.AreEqual(0, player.Location);
        }

        [TestMethod]
        public void IncrementRoundsPlayedIncreasesRoundsPlayedByOne()
        {
            var player = new Player("Lucas");

            player.IncrementRoundsPlayed();

            Assert.AreEqual(1, player.RoundsPlayed);
        }
    }
}
