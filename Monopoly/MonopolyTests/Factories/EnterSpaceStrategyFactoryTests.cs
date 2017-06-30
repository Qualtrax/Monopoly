using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Factories;
using Monopoly.Spaces;
using Monopoly.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly;

namespace MonopolyTests.Factories
{
    [TestClass]
    public class EnterSpaceStrategyFactoryTests
    {
        private EnterSpaceStrategyFactory factory;
        private Player player;

        public EnterSpaceStrategyFactoryTests()
        {
            factory = new EnterSpaceStrategyFactory();
            player = new Player("Luke");
        }

        [TestMethod]
        public void CreateForGenericSpaceReturnsGenericStrategy()
        {
            var genericSpace = new GenericSpace();

            var resultStrategy = factory.Create(genericSpace, player);

            Assert.IsInstanceOfType(resultStrategy, typeof(GenericEnterSpaceStrategy));
        }

        [TestMethod]
        public void CreateForGoSpaceReturnsGoStrategy()
        {
            var goSpace = new GoSpace();

            var resultStrategy = factory.Create(goSpace, player);

            Assert.IsInstanceOfType(resultStrategy, typeof(GoEnterSpaceStrategy));
        }

        [TestMethod]
        public void CreateForGoToJailSpaceReturnsGoToJailStrategy()
        {
            var goToJailSpace = new GoToJailSpace();

            var resultStrategy = factory.Create(goToJailSpace, player);

            Assert.IsInstanceOfType(resultStrategy, typeof(GoToJailEnterSpaceStrategy));
        }

        [TestMethod]
        public void CreateForJustVisitingSpaceReturnsJustVisitingStrategy()
        {
            var justVisitingSpace = new JustVisitingSpace();

            var resultStrategy = factory.Create(justVisitingSpace, player);

            Assert.IsInstanceOfType(resultStrategy, typeof(JustVisitingEnterSpaceStrategy));
        }
    }
}
