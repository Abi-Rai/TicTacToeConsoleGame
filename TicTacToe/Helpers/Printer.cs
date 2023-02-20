using System.Text;
using TicTacToe.Game.Board;
using TicTacToe.Game.Player;

namespace TicTacToe.Helpers
{
    public static class Printer
    {
        public static void Write(string text)
        {
            Console.Write(text);
        }
        public static void WriteLine()
        {
            Console.WriteLine();
        }
        public static void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
        public static string? ReadLine()
        {
            return Console.ReadLine();
        }
        public static ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }
        public static void SetForegroundColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
        public static void DisplayValidKeys(char[] validKeys)
        {
            SetForegroundColor(ConsoleColor.Cyan);
            Console.WriteLine($"""

                 Please only select keys from these options 
                 ( {string.Join(" , ", validKeys)} )
                """);
            SetForegroundColor(ConsoleColor.White);
        }
        public static void Greet(GameBoard board, Player playerOne, Player playerTwo)
        {
            ClearConsole();
            SetForegroundColor(ConsoleColor.White);
            string message = $"""
                Hello welcome to Tic-Tac-Toe game!

                {GetSettings(board, playerOne, playerTwo)}

                Options:
                1) Start Game
                2) Change Settings
                3) Exit

                Please press select one of the options available
                """;
            WriteLine(message);
        }
        public static void DisplaySettings(GameBoard board, Player playerOne, Player playerTwo)
        {
            ClearConsole();
            SetForegroundColor(ConsoleColor.Yellow);
            WriteLine(GetSettings(board, playerOne, playerTwo));
            string settingChange = $"""
                Choose which setting to change:
                1) Change board size

                2) Change {playerOne.Name} name
                3) Change {playerOne.Name} marker

                4) Change {playerTwo.Name} name
                5) Change {playerTwo.Name} marker
                """;
            WriteLine(settingChange);
        }
        private static string GetSettings(GameBoard board, Player playerOne, Player playerTwo)
        {
            string message = $"""
                Your current settings are

                   BoardSettings:-

                    BoardSize: {board.RowLength} x {board.RowLength} = {board.BoardSize} cells

                   PlayerSettings:-

                    Player 1 Name = {playerOne.Name} : marker = {playerOne.Marker}
                    Player 2 Name = {playerTwo.Name} : marker = {playerTwo.Marker}

                """;
            return message;
        }
        public static void DrawBoard(GameBoard board)
        {
            Cell[] boardCells = board.Board;
            SetForegroundColor(ConsoleColor.White);
            ClearConsole();
            int rowLength = (int)Math.Sqrt(boardCells.Length);
            StringBuilder sb = new();
            sb.AppendLine("\n-----------------------------------------------------------");
            sb.AppendLine("** At any point to exit game type exit and press enter **");
            sb.AppendLine("-----------------------------------------------------------\n");
            sb.AppendLine(new String('_', boardCells.Length + rowLength + 1));
            sb.AppendLine();
            int index = 0;
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    sb.Append($"|{(boardCells[index++].ValueStr).ToString().PadCenterExtension(rowLength)}");
                }
                sb.AppendLine("|");
                sb.AppendLine(new String('_', boardCells.Length + rowLength + 1));
            }
            sb.AppendLine("\n-----------------------------------------------------------");
            WriteLine(sb.ToString());

            if (board.MoveHistory.Any())
            {
                ShowLastMove(board);
            }
        }
        private static void ShowLastMove(GameBoard board)
        {
            SetForegroundColor(ConsoleColor.Green);
            WriteLine($"LastRound: {board.MoveHistory.Peek()}");
            SetForegroundColor(ConsoleColor.White);
            WriteLine("\n-----------------------------------------------------------");
        }

        private static void ClearConsole() => Console.Clear();

        private static string PadCenterExtension(this string str, int width, char padChar = ' ')
        {
            if (width <= str.Length) return str;
            int padding = width - str.Length;
            return str.PadLeft(str.Length + padding / 2, padChar)
                      .PadRight(width, padChar);
        }
        public static void DisplayExitMessage() => WriteLine("\nThank you for playing Tic-Tac-Toe\n");

        public static void DisplayBoardOptions(GameBoard board)
        {
            ClearConsole();
            string message = $"""
                Current board size is:-
                BoardSize: {board.RowLength} x {board.RowLength} = {board.BoardSize}

                Choose which boardsize to change to:
                2) 2x2 = 4
                3) 3x3 = 9
                4) 4x4 = 16
                5) 5x5 = 25

                *Note* This will reinstantiate the game board in the selected size
                """;
            WriteLine(message);
        }

        public static void DisplayNameChooseMessage(Player player)
        {
            WriteLine($"\nPlease choose a new name for => {player.Name}\n**New name cannot be empty or an exisiting name");
        }

        public static void DisplayMarkerChooser(Player player)
        {
            string message = $"""

                Please choose a new marker for {player.Name} current market= {player.Marker}
                **New marker can only be 1 character long and cannot be the same as other players marker
                """;
            WriteLine(message);
        }

        public static void DisplayAvailableCells(string[] availableCells)
        {
            string message = $"""
                please choose from one of these available cells, by typing in the number and pressing enter:-
                {string.Join(", ", availableCells)}
                """;
            WriteLine(message);
        }

        public static void DisplayInvalidStringMessage(string[] availableCells)
        {
            string message = $"""
                Invalid input! 
                Input can only be one of the available cells or "exit"

                """;
            WriteLine(message);
            DisplayAvailableCells(availableCells);
        }
        public static void DisplayInvalidMarkerMessage() => WriteLine(" Invalid input! New marker can only be 1 character long and cannot be the same as current players markers");
        public static void DisplayInvalidKeyMessage(char[] validKeys)
        {
            WriteLine(" Incorrect key pressed!");
            DisplayValidKeys(validKeys);
        }
        public static void DisplayInvalidNameMessage() => WriteLine(" Invalid input! Name cannot be empty.");

        public static void DisplayWinnerMessage(Player currentPlayer, GameBoard board)
        {
            DrawBoard(board);
            SetForegroundColor(ConsoleColor.Green);
            string message = $"""
                player: {currentPlayer.Name} has won, Congratulations :)
                The game lasted for a total of {board.TotalMoves} moves
                """;
            WriteLine(message);
        }

        public static void DisplayDrawMessage(GameBoard board)
        {
            SetForegroundColor(ConsoleColor.Green);
            string message = $"""
                The game has ended in a draw. Nobody won :(
                """;
            WriteLine(message);
        }
        public static void DisplayRestartGameMessage()
        {
            SetForegroundColor(ConsoleColor.White);
            string message = $"""

                Would you like to play again? 
                1) Yes
                2) No


                """;
            WriteLine(message);
        }
        public static void DisplayCurrentPlayerTurn(Player player)
        {
            SetForegroundColor(ConsoleColor.Magenta);
            string message = $"""
                Player: {player.Name}'s turn
                """;
            WriteLine(message);
            SetForegroundColor(ConsoleColor.White);
        }
        public static void DisplayPlayAgainstComputerMessage()
        {
            SetForegroundColor(ConsoleColor.Magenta);
            string message = $"""

                Would you like to play against Computer?
                1) Yes
                2) No

                """;
            WriteLine(message);
            SetForegroundColor(ConsoleColor.White);
        }
        public static void DisplayPlayerMove(Player player, string playerInput)
        {
            SetForegroundColor(ConsoleColor.Green);
            string message = $"""

                Player:{player.Name} put their marker on cell:{playerInput}
              
                """;
            WriteLine(message);
            SetForegroundColor(ConsoleColor.White);
        }
        public static void DisplayFirstOrSecondMessage()
        {
            SetForegroundColor(ConsoleColor.Green);
            string message = $"""

            Do you want to play first?
            1) Yes
            2) No
            """;
            WriteLine(message);
            SetForegroundColor(ConsoleColor.White);
        }
    }
}
