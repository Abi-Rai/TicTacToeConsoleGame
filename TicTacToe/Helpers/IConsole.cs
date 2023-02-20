namespace TicTacToe.Helpers
{
    public interface IConsole
    {
        void Write(string text);
        void WriteLine();
        void WriteLine(string text);
        string? ReadLine();
        ConsoleKeyInfo ReadKey();
        void SetForegroundColor(ConsoleColor color);
        void Clear();
    }
}