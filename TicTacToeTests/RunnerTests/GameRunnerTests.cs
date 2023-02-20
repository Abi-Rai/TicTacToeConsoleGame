using Moq;
using TicTacToe.Game.Board;
using TicTacToe.Helpers;
using TicTacToe.Runner;

namespace TicTacToeTests.RunnerTests
{
    public class GameRunnerTests
    {
        private readonly GameRunner _runner;
        private readonly Mock<IPrinter> _mockPrinter;

        public GameRunnerTests()
        {
            _runner = new GameRunner();
            _mockPrinter = new Mock<IPrinter>();

        }
    }
}
