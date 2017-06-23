using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Factories;
using Monopoly.Spaces;
using Monopoly.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTests.Factories
{
    [TestClass]
    public class EnterSpaceStrategyFactoryTests
    {
        private EnterSpaceStrategyFactory factory;

        public EnterSpaceStrategyFactoryTests()
        {
            factory = new EnterSpaceStrategyFactory();
        }

        [TestMethod]
        public void CreateForGenericSpaceReturnsGenericStrategy()
        {
            var genericSpace = new GenericSpace();

            var resultStrategy = factory.Create(genericSpace);

            Assert.IsInstanceOfType(resultStrategy, typeof(GenericEnterSpaceStrategy));
        }

        [TestMethod]
        public void CreateForGoSpaceReturnsGoStrategy()
        {
            var goSpace = new GoSpace();

            var resultStrategy = factory.Create(goSpace);

            Assert.IsInstanceOfType(resultStrategy, typeof(GoEnterSpaceStrategy));
        }

        [TestMethod]
        public void CreateForGoToJailSpaceReturnsGoToJailStrategy()
        {
            var goToJailSpace = new GoToJailSpace();

            var resultStrategy = factory.Create(goToJailSpace);

            Assert.IsInstanceOfType(resultStrategy, typeof(GoToJailEnterSpaceStrategy));
        }

        [TestMethod]
        public void CreateForJustVisitingSpaceReturnsJustVisitingStrategy()
        {
            var justVisitingSpace = new JustVisitingSpace();

            var resultStrategy = factory.Create(justVisitingSpace);

            Assert.IsInstanceOfType(resultStrategy, typeof(JustVisitingEnterSpaceStrategy));
        }
    }
}
