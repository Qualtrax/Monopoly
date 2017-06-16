using System;

namespace Monopoly.Services
{
    public interface IMovementService
    {
        void MovePlayer(Player player, Int32 spacesToMove);
    }
}