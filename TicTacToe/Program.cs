using TicTacToe.Runner;

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
            GameRunner game = new();
            return game;
        }
    }
}