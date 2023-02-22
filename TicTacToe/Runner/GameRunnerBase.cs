using TicTacToe.Helpers;
using TicTacToe.Helpers.Interfaces;

namespace TicTacToe.Runner
{
    public class GameRunnerBase
    {
        public static IPrinter Printer { get; set; } = new Printer();
        public static IInputParser InputParser { get; set; } = new InputParser();
    }
}