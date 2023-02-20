using TicTacToe.Game.Board;

namespace TicTacToe.Helpers
{
    public static class StaticPrinter
    {
        private static readonly IPrinter _printer = new Printer();
        public static void Write(string text) => _printer.Write(text);
        public static void WriteLine() => _printer.WriteLine();
        public static void WriteLine(string text) => _printer.WriteLine(text);
        public static string? ReadLine() => _printer.ReadLine();
        public static ConsoleKeyInfo ReadKey() => _printer.ReadKey();
        public static void SetForegroundColor(ConsoleColor color) => _printer.SetForegroundColor(color);

        public static void DisplayValidKeys(char[] validKeys) => _printer.DisplayValidKeys(validKeys);
        public static void Greet(GameBoard board, Player playerOne, Player playerTwo) => _printer.Greet(board, playerOne, playerTwo);
        public static void DisplaySettings(GameBoard board, Player playerOne, Player playerTwo) => _printer.DisplaySettings(board, playerOne, playerTwo);

        public static void DrawBoard(GameBoard board) => _printer.DrawBoard(board);

        public static void DisplayExitMessage() => _printer.DisplayExitMessage();

        public static void DisplayBoardOptions(GameBoard board) => _printer.DisplayBoardOptions(board);

        public static void DisplayNameChooseMessage(Player player) => _printer.DisplayNameChooseMessage(player);

        public static void DisplayMarkerChooser(Player player) => _printer.DisplayMarkerChooser(player);

        public static void DisplayAvailableCells(string[] availableCells) => _printer.DisplayAvailableCells(availableCells);

        public static void DisplayInvalidStringMessage(string[] availableCells) => _printer.DisplayInvalidStringMessage(availableCells);
        public static void DisplayInvalidMarkerMessage() => _printer.DisplayInvalidMarkerMessage();
        public static void DisplayInvalidKeyMessage(char[] validKeys) => _printer.DisplayInvalidKeyMessage(validKeys);
        public static void DisplayInvalidNameMessage() => _printer.DisplayInvalidNameMessage();

        public static void DisplayWinnerMessage(Player currentPlayer, GameBoard board) => _printer.DisplayWinnerMessage(currentPlayer, board);

        public static void DisplayDrawMessage(GameBoard board) => _printer.DisplayDrawMessage(board);
        public static void DisplayRestartGameMessage() => _printer.DisplayRestartGameMessage();
        public static void DisplayCurrentPlayerTurn(Player player) => _printer.DisplayCurrentPlayerTurn(player);
        public static void DisplayPlayAgainstComputerMessage() => _printer.DisplayPlayAgainstComputerMessage();
        public static void DisplayPlayerMove(Player player, string playerInput) => _printer.DisplayPlayerMove(player, playerInput);
        public static void DisplayFirstOrSecondMessage() => _printer.DisplayFirstOrSecondMessage();
    }
}
