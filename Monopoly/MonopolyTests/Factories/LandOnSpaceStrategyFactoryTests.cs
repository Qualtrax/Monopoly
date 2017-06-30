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
    public class LandOnSpaceStrategyFactoryTests
    {
        [TestClass]
        public class LandOnStrategyFactoryTests
        {
            private ILandOnSpaceStrategyFactory factory;
            private Player player;

            public LandOnStrategyFactoryTests()
            {
                factory = new LandOnSpaceStrategyFactory();
                player = new Player("Lucas");
            }

            [TestMethod]
            public void CreateForGenericSpaceReturnsGenericStrategy()
            {
                var genericSpace = new GenericSpace();

                var resultStrategy = factory.Create(genericSpace, player);

                Assert.IsInstanceOfType(resultStrategy, typeof(GenericLandOnSpaceStrategy));
            }

            [TestMethod]
            public void CreateForGoSpaceReturnsGoStrategy()
            {
                var goSpace = new GoSpace();

                var resultStrategy = factory.Create(goSpace, player);

                Assert.IsInstanceOfType(resultStrategy, typeof(GoLandOnSpaceStrategy));
            }

            [TestMethod]
            public void CreateForGoToJailSpaceReturnsGoToJailStrategy()
            {
                var goToJailSpace = new GoToJailSpace();

                var resultStrategy = factory.Create(goToJailSpace, player);

                Assert.IsInstanceOfType(resultStrategy, typeof(GoToJailLandOnSpaceStrategy));
            }

            [TestMethod]
            public void CreateForJustVisitingSpaceReturnsJustVisitingStrategy()
            {
                var justVisitingSpace = new JustVisitingSpace();

                var resultStrategy = factory.Create(justVisitingSpace, player);

                Assert.IsInstanceOfType(resultStrategy, typeof(JustVisitingLandOnSpaceStrategy));
            }
        }
    }
}
