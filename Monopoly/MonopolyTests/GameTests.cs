using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Monopoly;

namespace MonopolyTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Cannot start game with less than 2 players")]
        public void CreateAndPlayGameWithOnePlayerFails()
        {
            var players = new[] { "Tim" };
            var game = new Game();
            game.Play(players);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Cannot start game with more than 8 players")]
        public void CreateAndPlayGameWithNinePlayersFails()
        {
            var players = new[] { "Tim", "Richard", "Dick", "Robert", "Bob",
                "Kevin", "David", "Lucas", "Luke"};
            var game = new Game();
            game.Play(players);
        }

        [TestMethod]
        public void CreateHorseAndCarBothNamingOrdersOccurWithin100Games()
        {
            var horseThenCarOccurs = false;
            var carThenHorseOccurs = false;
            var carThenHorseList = new List<String> { "Car", "Horse" };
            var horseThenCarList = new List<String> { "Horse", "Car" };
            var game = new Game();

            for (int i = 0; i < 100; i++)
            {
                game.Play(carThenHorseList.ToArray());
                var randomizedPlayerList = game.GetPlayerNames().ToList<String>();

                if (carThenHorseList.SequenceEqual<String>(randomizedPlayerList))
                    horseThenCarOccurs = true;
                else if (horseThenCarList.SequenceEqual<String>(randomizedPlayerList))
                    carThenHorseOccurs = true;
            }

            Assert.IsTrue(horseThenCarOccurs);
            Assert.IsTrue(carThenHorseOccurs);
        }
    }
}
