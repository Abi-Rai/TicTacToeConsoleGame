using TicTacToe.CustomException;
using TicTacToe.Game.Board;
using TicTacToe.Helpers;
using TicTacToe.Runner.Enums;
using TicTacToe.Runner.Utils;

namespace TicTacToe.Runner
{

    public class GameRunner
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
                GameRunnerHelpers.Exit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error has occured:{0}, stacktrace:{1}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        private void ShowMainMenu()
        {
            StaticPrinter.Greet(_board, _playerOne, _playerTwo);

            var playerInput = (StartGameOptions)GameRunnerHelpers.WaitPlayerValidKeyInput('1', '3');
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
            StaticPrinter.DisplaySettings(_board, _playerOne, _playerTwo);
            var playerInput = (ChangeSettingOptions)GameRunnerHelpers.WaitPlayerValidKeyInput('1', '5');
            switch (playerInput)
            {
                case ChangeSettingOptions.ChangeBoardSize:
                    BoardSettings();
                    break;
                case ChangeSettingOptions.ChangePlayerOneName:
                    StaticPrinter.DisplayNameChooseMessage(_playerOne);
                    _playerOne.ChangeName(WaitPlayerValidName());
                    break;
                case ChangeSettingOptions.ChangePlayerTwoName:
                    StaticPrinter.DisplayNameChooseMessage(_playerTwo);
                    _playerTwo.ChangeName(WaitPlayerValidName());
                    break;
                case ChangeSettingOptions.ChangePlayerOneMarker:
                    StaticPrinter.DisplayMarkerChooser(_playerOne);
                    _playerOne.ChangeMarker(WaitplayerValidMarker());
                    break;
                case ChangeSettingOptions.ChangePlayerTwoMarker:
                    StaticPrinter.DisplayMarkerChooser(_playerTwo);
                    _playerTwo.ChangeMarker(WaitplayerValidMarker());
                    break;
            }
            ShowMainMenu();
        }

        private string WaitplayerValidMarker()
        {
            var playerInput = StaticPrinter.ReadLine();
            while (PlayerInputParser.IsValidMarkerInput(playerInput, _playerOne.Marker, _playerTwo.Marker) == false)
            {
                StaticPrinter.DisplayInvalidMarkerMessage();
                playerInput = StaticPrinter.ReadLine();
            }
            return playerInput!.Trim().ToString();
        }

        private void BoardSettings()
        {
            StaticPrinter.DisplayBoardOptions(_board);
            var playerInput = (BoardSizeOptions)GameRunnerHelpers.WaitPlayerValidKeyInput('2', '5');

            var rowSizeToBoardSizeOptions = (BoardSizeOptions)Enum.Parse(typeof(BoardSizeOptions), _board.RowSize.ToString());

            if (rowSizeToBoardSizeOptions != playerInput)
            {
                var boardSizeToRowSize = (GameBoard.RowSizes)Enum.Parse(typeof(GameBoard.RowSizes), playerInput.ToString());
                _board = new GameBoard(boardSizeToRowSize);
            }
        }

        private void StartGame()
        {
            bool gameNotEnd = true;
            bool playerOneTurn = true;
            PlayAgainstComputer();

            while (gameNotEnd)
            {
                StaticPrinter.DrawBoard(_board);
                Player currentPlayer = playerOneTurn ? _playerOne : _playerTwo;
                StaticPrinter.DisplayCurrentPlayerTurn(currentPlayer);
                string[] availableCells = GetAvailableCells();

                string playerInput;
                if (currentPlayer.Human)
                {
                    StaticPrinter.DisplayAvailableCells(availableCells);
                    playerInput = GameRunnerHelpers.WaitPlayerValidStringInput(availableCells);
                }
                else
                {
                    playerInput = GameRunnerHelpers.GetComputersMove(availableCells);
                }

                if (_board.MakeMove(currentPlayer, playerInput))
                {
                    StaticPrinter.DisplayWinnerMessage(currentPlayer, _board);
                    gameNotEnd = false;
                }
                else if (_board.TotalMoves >= _board.BoardSize)
                {
                    StaticPrinter.DisplayDrawMessage(_board);
                    gameNotEnd = false;
                }
                else
                {
                    playerOneTurn = !playerOneTurn;
                }
            }
            ShowRestartOption();
        }

        private bool PlayAgainstComputer()
        {
            StaticPrinter.DisplayPlayAgainstComputerMessage();
            var playAgainstComputerChoice = (YesNoOption)GameRunnerHelpers.WaitPlayerValidKeyInput('1', '2');
            if (playAgainstComputerChoice == YesNoOption.Yes)
            {
                StaticPrinter.DisplayFirstOrSecondMessage();
                var playFirstChoice = (YesNoOption)GameRunnerHelpers.WaitPlayerValidKeyInput('1', '2');
                if (playFirstChoice == YesNoOption.Yes)
                {
                    _playerTwo.SetPlayerAsComputer();
                }
                else
                {
                    _playerOne.SetPlayerAsComputer();
                }
            }
            return false;
        }

        private void ShowRestartOption()
        {
            StaticPrinter.DisplayRestartGameMessage();
            var playerInput = (YesNoOption)GameRunnerHelpers.WaitPlayerValidKeyInput('1', '2');
            if (playerInput == YesNoOption.Yes)
            {
                _board = new GameBoard(_board.RowSize);
                ShowMainMenu();
            }

            GameRunnerHelpers.Exit();
        }

        private string[] GetAvailableCells()
        {
            return _board.Board.Where(c => !c.IsPlayerSet)
                                .Select(c => c.ValueStr)
                                .ToArray();
        }

        private string WaitPlayerValidName()
        {
            var playerInput = StaticPrinter.ReadLine();
            while (PlayerInputParser.IsValidNameInput(playerInput, _playerOne.Name, _playerTwo.Name) == false)
            {
                StaticPrinter.DisplayInvalidNameMessage();
                playerInput = StaticPrinter.ReadLine();
            }
            return playerInput!.ToString();
        }
    }
}
