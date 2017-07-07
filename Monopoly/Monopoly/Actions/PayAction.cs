using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Actions
{
    public class PayAction : IAction
    {
        public Int32 Amount { get; private set; }

        public PayAction(Int32 amount)
        {
            Amount = amount;
        }

    }
}
