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
        public void CreatePlayerInitialBalanceIsZero()
        {
            Assert.AreEqual(0, player.Balance);
        }

        [TestMethod]
        public void IncrementRoundsPlayedIncreasesRoundsPlayedByOne()
        {
            var player = new Player("Lucas");

            player.RoundsPlayed++;

            Assert.AreEqual(1, player.RoundsPlayed);
        }
        [TestMethod]
        public void AddsOneHundredToPlayerOneHundredTotal()
        {
            var player = new Player("Lucas");
            var fundsToAdd = 100;

            player.Balance += fundsToAdd;

            Assert.AreEqual(fundsToAdd, player.Balance);
        }

        [TestMethod]
        public void PlayerBalanceStartsAtOneHundredRemoveSeventyHasThirtyLeft()
        {
            var player = new Player("Lucas");
            var fundsToAdd = 100;
            var fundsToRemove = 70;

            player.Balance += fundsToAdd;
            player.Balance -= fundsToRemove;

            Assert.AreEqual(fundsToAdd - fundsToRemove, player.Balance);
        }
    }
}
