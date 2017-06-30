﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Strategies
{
    public class GoEnterSpaceStrategy : IEnterSpaceStrategy
    {
        private Player player;

        public GoEnterSpaceStrategy(Player player)
        {
            this.player = player;
        }

        public void Act()
        {
            player.Balance += 200;
        }
    }
}