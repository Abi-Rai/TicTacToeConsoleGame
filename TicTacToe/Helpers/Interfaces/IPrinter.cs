using TicTacToe.Game.Board;

namespace TicTacToe.Helpers.Interfaces
{
    public interface IPrinter
    {
        void DisplayAvailableCells(string[] availableCells);
        void DisplayBoardOptions(GameBoard board);
        void DisplayCurrentPlayerTurn(Player player);
        void DisplayDrawMessage(GameBoard board);
        void DisplayExitMessage();
        void DisplayFirstOrSecondMessage();
        void DisplayInvalidKeyMessage(char[] validKeys);
        void DisplayInvalidMarkerMessage();
        void DisplayInvalidNameMessage();
        void DisplayInvalidStringMessage(string[] availableCells);
        void DisplayMarkerChooser(Player player);
        void DisplayNameChooseMessage(Player player);
        void DisplayPlayAgainstComputerMessage();
        void DisplayPlayerMove(Player player, string playerInput);
        void DisplayRestartGameMessage();
        void DisplaySettings(GameBoard board, Player playerOne, Player playerTwo);
        void DisplayValidKeys(char[] validKeys);
        void DisplayWinnerMessage(Player currentPlayer, GameBoard board);
        void DrawBoard(GameBoard board);
        void Greet(GameBoard board, Player playerOne, Player playerTwo);
        ConsoleKeyInfo ReadKey();
        string? ReadLine();
        void SetForegroundColor(ConsoleColor color);
        void Write(string text);
        void WriteLine();
        void WriteLine(string text);
    }
}