using System;

namespace Monopoly.Services
{
    public interface ITurnService
    {
        void TakeTurn(Player player, Int32 spacesToMove);
    }
}