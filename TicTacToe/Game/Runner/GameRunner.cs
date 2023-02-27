using TicTacToe.CustomException;
using TicTacToe.Game.Board;
using TicTacToe.Game.Board.Enums;
using TicTacToe.Game.Runner.Enums;
using TicTacToe.Game.Runner.Utils;

namespace TicTacToe.Game.Runner
{

    public class GameRunner : GameRunnerBase
    {
        private GameBoard _board;
        private readonly Player _playerOne;
        private readonly Player _playerTwo;
        public GameRunner()
        {
            _board = new GameBoard();
            _playerOne = new Player("Player1", "X");
            _playerTwo = new Player("Player2", "O");
        }

        public void Run()
        {
            try
            {
                ShowMainMenu();
            }
            catch (UserRequestExitException ex)
            {
                Console.WriteLine("\nExiting game... reason: {0}", ex.Message);
                GameRunnerHelper.Exit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error has occured:{0}, stacktrace:{1}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        private void ShowMainMenu()
        {
            Printer.Greet(_board, _playerOne, _playerTwo);

            var playerInput = (StartGameOptions)GameRunnerHelper.WaitPlayerValidKeyInput((char)StartGameOptions.StartGame, (char)StartGameOptions.Exit);
            switch (playerInput)
            {
                case StartGameOptions.StartGame:
                    StartGame();
                    break;
                case StartGameOptions.ChangeSettings:
                    ChangeSettings();
                    break;
                case StartGameOptions.Exit:
                    throw new UserRequestExitException("User request exit game");
            }
        }


        private void ChangeSettings()
        {
            Printer.DisplaySettings(_board, _playerOne, _playerTwo);
            var playerInput = (ChangeSettingOptions)GameRunnerHelper.WaitPlayerValidKeyInput((char)ChangeSettingOptions.ChangeBoardSize, (char)ChangeSettingOptions.ChangePlayerTwoMarker);
            switch (playerInput)
            {
                case ChangeSettingOptions.ChangeBoardSize:
                    BoardSettings();
                    break;
                case ChangeSettingOptions.ChangePlayerOneName:
                    Printer.DisplayNameChooseMessage(_playerOne);
                    _playerOne.ChangeName(WaitPlayerValidName());
                    break;
                case ChangeSettingOptions.ChangePlayerTwoName:
                    Printer.DisplayNameChooseMessage(_playerTwo);
                    _playerTwo.ChangeName(WaitPlayerValidName());
                    break;
                case ChangeSettingOptions.ChangePlayerOneMarker:
                    Printer.DisplayMarkerChooser(_playerOne);
                    _playerOne.ChangeMarker(WaitplayerValidMarker());
                    break;
                case ChangeSettingOptions.ChangePlayerTwoMarker:
                    Printer.DisplayMarkerChooser(_playerTwo);
                    _playerTwo.ChangeMarker(WaitplayerValidMarker());
                    break;
            }
            ShowMainMenu();
        }

        private string WaitplayerValidMarker()
        {
            var playerInput = Printer.ReadLine();
            while (InputParser.IsValidMarkerInput(playerInput, _playerOne.Marker, _playerTwo.Marker) == false)
            {
                Printer.DisplayInvalidMarkerMessage();
                playerInput = Printer.ReadLine();
            }
            return playerInput!.Trim().ToString();
        }

        private void BoardSettings()
        {
            Printer.DisplayBoardOptions(_board);
            var playerInput = (BoardSizeOptions)GameRunnerHelper.WaitPlayerValidKeyInput((char)BoardSizeOptions.Two, (char)BoardSizeOptions.Five);

            var rowSizeToBoardSizeOptions = (BoardSizeOptions)Enum.Parse(typeof(BoardSizeOptions), _board.RowSize.ToString());

            if (rowSizeToBoardSizeOptions != playerInput)
            {
                var boardSizeToRowSize = (RowSizes)Enum.Parse(typeof(RowSizes), playerInput.ToString());
                _board = new GameBoard(boardSizeToRowSize);
            }
        }

        private void StartGame()
        {
            bool gameNotEnd = true;
            bool playerOneTurn = false;
            PlayAgainstComputer();

            while (gameNotEnd)
            {
                playerOneTurn = !playerOneTurn;
                Printer.DrawBoard(_board);
                Player currentPlayer = playerOneTurn ? _playerOne : _playerTwo;
                Printer.DisplayCurrentPlayerTurn(currentPlayer);
                string[] availableCells = GetAvailableCells();

                string currentPlayerInput = GetCurrentPlayerInput(currentPlayer, availableCells);

                if (MakeMoveCheckWin(currentPlayer, currentPlayerInput))
                {
                    Printer.DisplayWinnerMessage(currentPlayer, _board);
                    gameNotEnd = false;
                }
                else if (CheckDraw())
                {
                    Printer.DisplayDrawMessage(_board);
                    gameNotEnd = false;
                }
            }
            ShowRestartOption();
        }

        private bool CheckDraw() => _board.TotalMoves >= _board.BoardSize;

        private bool MakeMoveCheckWin(Player currentPlayer, string currentPlayerInput)
        {
            return _board.MakeMove(currentPlayer, currentPlayerInput);
        }

        private static string GetCurrentPlayerInput(Player currentPlayer, string[] availableCells)
        {
            if (currentPlayer.Human)
            {
                Printer.DisplayAvailableCells(availableCells);
                return GameRunnerHelper.WaitPlayerValidStringInput(availableCells);
            }
            else
            {
                return GameRunnerHelper.GetComputersMove(availableCells);
            }
        }

        private void PlayAgainstComputer()
        {
            Printer.DisplayPlayAgainstComputerMessage();
            var playAgainstComputerChoice = (YesNoOption)GameRunnerHelper.WaitPlayerValidKeyInput((char)YesNoOption.Yes, (char)YesNoOption.No);
            if (playAgainstComputerChoice == YesNoOption.Yes)
            {
                Printer.DisplayFirstOrSecondMessage();
                var playFirstChoice = (YesNoOption)GameRunnerHelper.WaitPlayerValidKeyInput((char)YesNoOption.Yes, (char)YesNoOption.No);
                if (playFirstChoice == YesNoOption.Yes)
                {
                    _playerTwo.SetPlayerAsComputer();
                }
                else
                {
                    _playerOne.SetPlayerAsComputer();
                }
            }
        }

        private void ShowRestartOption()
        {
            Printer.DisplayRestartGameMessage();

            var playerInput = (YesNoOption)GameRunnerHelper.WaitPlayerValidKeyInput((char)YesNoOption.Yes, (char)YesNoOption.No);
            if (playerInput == YesNoOption.Yes)
            {
                _board = new GameBoard(_board.RowSize);
                SetComputerBackToHuman();
                ShowMainMenu();
            }

            GameRunnerHelper.Exit();
        }

        private void SetComputerBackToHuman() => (_playerOne.Human ? _playerTwo : _playerOne).SetPlayerBackToHuman();

        private string[] GetAvailableCells()
        {
            return _board.Board.Where(c => !c.IsPlayerSet)
                                .Select(c => c.ValueStr)
                                .ToArray();
        }

        private string WaitPlayerValidName()
        {
            var playerInput = Printer.ReadLine();
            while (InputParser.IsValidNameInput(playerInput, _playerOne.Name, _playerTwo.Name) == false)
            {
                Printer.DisplayInvalidNameMessage();
                playerInput = Printer.ReadLine();
            }
            return playerInput!.ToString();
        }
    }
}
