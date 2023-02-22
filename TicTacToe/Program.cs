using TicTacToe.Helpers;
using TicTacToe.Runner;
using TicTacToe.Runner.Utils;

namespace TicTacToe
{
    internal class Program
    {
        static void Main()
        {
            GameRunner game = SetupGameRunner();
            game.Run();
        }
        private static GameRunner SetupGameRunner()
        {
            GameRunnerBase.Printer = new Printer();
            GameRunner game = new();
            return game;
        }
    }
}