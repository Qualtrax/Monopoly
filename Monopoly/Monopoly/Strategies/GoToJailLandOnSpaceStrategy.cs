using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Strategies
{
    public class GoToJailLandOnSpaceStrategy : ILandOnSpaceStrategy
    {
        private Player player;

        public GoToJailLandOnSpaceStrategy(Player player)
        {
            this.player = player;
        }
        public void Act()
        {
            player.Location = MonopolyConstants.JailLocation;
        }
    }
}
