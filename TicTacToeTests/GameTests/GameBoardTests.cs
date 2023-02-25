using FluentAssertions;
using TicTacToe.Game.Board;

namespace TicTacToeTests.GameTests
{
    public class GameBoardTests
    {
        private readonly GameBoard _gameBoard;
        public GameBoardTests()
        {
            _gameBoard = new GameBoard();
        }
        [Fact]
        public void GameBoard_ShouldReturn_DefaultArrayOfCells()
        {
            //Arrange
            Cell[] expectedCellArray = new Cell[]
            {
                new Cell("1", 0, false),
                new Cell("2", 1, false),
                new Cell("3", 2, false),
                new Cell("4", 3, false),
                new Cell("5", 4, false),
                new Cell("6", 5, false),
                new Cell("7",6,false),
                new Cell("8",7,false),
                new Cell("9",8,false)
            };
            //Act 
            Cell[] boardUnderTest = _gameBoard.Board;

            //Assert
            boardUnderTest.Should().BeEquivalentTo(expectedCellArray, options => options.WithStrictOrdering());
        }

        [Fact]
        public void GameBoard_ShouldNotReturn_WrongOrderOfCells()
        {
            //Arrange
            Cell[] unExpectedCellArray = new Cell[]
            {
                new Cell("9", 8, false),
                new Cell("8", 7, false),
                new Cell("7", 6, false),
                new Cell("6", 5, false),
                new Cell("5", 4, false),
                new Cell("4", 3, false),
                new Cell("3", 2, false),
                new Cell("2", 1, false),
                new Cell("1", 0, false)
            };
            //Act
            Cell[] boardUnderTest = _gameBoard.Board;

            //Assert
            boardUnderTest.Should().NotBeEquivalentTo(unExpectedCellArray, options => options.WithStrictOrdering());
        }
        [Fact]
        public void NewGame_ShouldReturn_DefaultArrayOfCells()
        {
            //Arrange
            Cell[] expectedCellArray = new Cell[]
            {
                new Cell("1", 0, false),
                new Cell("2", 1, false),
                new Cell("3", 2, false),
                new Cell("4", 3, false),
                new Cell("5", 4, false),
                new Cell("6", 5, false),
                new Cell("7",6,false),
                new Cell("8",7,false),
                new Cell("9",8,false)
            };

            //Act 
            Cell[] cellArrayUnderTest = _gameBoard.NewGame();

            //Assert
            cellArrayUnderTest.Should().BeEquivalentTo(expectedCellArray, options => options.WithStrictOrdering());
        }

        [Fact]
        public void NewGame_ShouldNotReturn_WrongOrderOfCells()
        {
            //Arrange
            Cell[] unExpectedCellArray = new Cell[]
            {
                new Cell("9", 8, false),
                new Cell("8", 7, false),
                new Cell("7", 6, false),
                new Cell("6", 5, false),
                new Cell("5", 4, false),
                new Cell("4", 3, false),
                new Cell("3", 2, false),
                new Cell("2", 1, false),
                new Cell("1", 0, false)
            };

            //Act 
            Cell[] cellArrayUnderTest = _gameBoard.NewGame();

            //Assert
            cellArrayUnderTest.Should().NotBeEquivalentTo(unExpectedCellArray, options => options.WithStrictOrdering());
        }
        [Fact]
        public void MakeMove_ShouldChange_ValueOfCorrectCellAndReturnFalse()
        {
            //Arrange
            Cell[] expectedCellArray = new Cell[]
            {
                new Cell("1", 0, false),
                new Cell("2", 1, false),
                new Cell("3", 2, false),
                new Cell("4", 3, false),
                new Cell("X", 4, true),
                new Cell("6", 5, false),
                new Cell("7",6,false),
                new Cell("8",7,false),
                new Cell("9",8,false)
            };

            Player givenPlayer = new("Player1", "X");
            string givenPlayerInput = "5";

            //Act 
            var result = _gameBoard.MakeMove(givenPlayer, givenPlayerInput);

            //Assert
            result.Should().BeFalse();
            _gameBoard.Board.Should().BeEquivalentTo(expectedCellArray, options => options.WithStrictOrdering());

        }
        [Fact]
        public void MakeMove_ShouldLogPlayerMove()
        {
            Player givenPlayer = new("Player1", "X");
            string givenPlayerInput = "5";
            Cell expectedCell = new("5", 4, true);
            Stack<string> expectedMoveHistory = new();
            expectedMoveHistory.Push($"player:{givenPlayer.Name} set their marker:{givenPlayer.Marker} on cell:{expectedCell.ValueStr}");

            //Act 
            var result = _gameBoard.MakeMove(givenPlayer, givenPlayerInput);

            //Assert
            _gameBoard.MoveHistory.Should().BeEquivalentTo(
                expectedMoveHistory,
                options => options.WithStrictOrdering());
            _gameBoard.TotalMoves.Should().Be(1);
        }
        [Fact]
        public void MakeMove_ShouldReturnTrue_WhenDiagonalGameWon()
        {
            //Arrange
            Cell[] expectedCellArray = new Cell[]
            {
                new Cell("X", 0, true),
                new Cell("O", 1, true),
                new Cell("3", 2, false),
                new Cell("4", 3, false),
                new Cell("X", 4, true),
                new Cell("O", 5, true),
                new Cell("7",6,false),
                new Cell("8",7,false),
                new Cell("X",8,true)
            };
            Player givenPlayerOne = new("Player1", "X");
            Player givenPlayerTwo = new("Player2", "O");

            //Act 
            _gameBoard.MakeMove(givenPlayerOne, "1");
            _gameBoard.MakeMove(givenPlayerTwo, "2");
            _gameBoard.MakeMove(givenPlayerOne, "9");
            _gameBoard.MakeMove(givenPlayerTwo, "6");
            var result = _gameBoard.MakeMove(givenPlayerOne, "5");

            //Assert
            result.Should().BeTrue();
            _gameBoard.TotalMoves.Should().Be(5);
            _gameBoard.Board.Should().BeEquivalentTo(expectedCellArray, options => options.WithStrictOrdering());
        }

        [Fact]
        public void MakeMove_ShouldReturnTrue_WhenRowGameWon()
        {
            //Arrange
            Cell[] expectedCellArray = new Cell[]
            {
                new Cell("O", 0, true),
                new Cell("O", 1, true),
                new Cell("3", 2, false),
                new Cell("X", 3, true),
                new Cell("X", 4, true),
                new Cell("X", 5, true),
                new Cell("7",6,false),
                new Cell("8",7,false),
                new Cell("9",8,false)
            };
            Player givenPlayerOne = new("Player1", "X");
            Player givenPlayerTwo = new("Player2", "O");

            //Act 
            _gameBoard.MakeMove(givenPlayerOne, "4");
            _gameBoard.MakeMove(givenPlayerTwo, "1");
            _gameBoard.MakeMove(givenPlayerOne, "5");
            _gameBoard.MakeMove(givenPlayerTwo, "2");
            var result = _gameBoard.MakeMove(givenPlayerOne, "6");

            //Assert
            result.Should().BeTrue();
            _gameBoard.TotalMoves.Should().Be(5);
            _gameBoard.Board.Should().BeEquivalentTo(expectedCellArray, options => options.WithStrictOrdering());
        }

        [Fact]
        public void MakeMove_ShouldReturnTrue_WhenColumnGameWon()
        {
            //Arrange
            Cell[] expectedCellArray = new Cell[]
            {
                new Cell("1", 0, false),
                new Cell("O", 1, true),
                new Cell("3", 2, false),
                new Cell("X", 3, true),
                new Cell("O", 4, true),
                new Cell("X", 5, true),
                new Cell("X",6,true),
                new Cell("O",7,true),
                new Cell("9",8,false)
            };
            Player givenPlayerOne = new("Player1", "X");
            Player givenPlayerTwo = new("Player2", "O");

            //Act 
            _gameBoard.MakeMove(givenPlayerOne, "4");
            _gameBoard.MakeMove(givenPlayerTwo, "2");
            _gameBoard.MakeMove(givenPlayerOne, "7");
            _gameBoard.MakeMove(givenPlayerTwo, "5");
            _gameBoard.MakeMove(givenPlayerOne, "6");
            var result = _gameBoard.MakeMove(givenPlayerTwo, "8");

            //Assert
            result.Should().BeTrue();
            _gameBoard.TotalMoves.Should().Be(6);
            _gameBoard.Board.Should().BeEquivalentTo(expectedCellArray, options => options.WithStrictOrdering());
        }
    }
}
