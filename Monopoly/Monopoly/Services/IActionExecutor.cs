using Monopoly.Strategies;

namespace Monopoly.Services
{
    public interface IActionExecutor
    {
        void Execute(ISpaceActionStrategy strategy, Player player);
    }
}
