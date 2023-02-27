using TicTacToe.Helpers;
using TicTacToe.Helpers.Interfaces;

namespace TicTacToe.Game.Runner
{
    public class GameRunnerBase
    {
        protected GameRunnerBase()
        {

        }
        public static IPrinter Printer { get; set; } = new Printer();
        public static IInputParser InputParser { get; set; } = new InputParser();
    }
}