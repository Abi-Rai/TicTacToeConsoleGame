using FluentAssertions;
using TicTacToe.Game.Board;

namespace TicTacToeTests.GameTests
{
    public class CellTests
    {
        [Fact]
        public void SetValue_ShouldChangeCorrectProperties()
        {
            //Arrange
            var cell = new Cell("5", 4, false);

            string newValue = "X";
            //Act 
            cell.SetValue(newValue);
            //Assert
            cell.ValueStr.Should().Be(newValue);
            cell.IsPlayerSet.Should().BeTrue();
        }

        [Fact]
        public void SetValue_ShouldNotChangeIndex()
        {
            //Arrange
            var cell = new Cell("3", 2, false);

            string newValue = "O";
            //Act 
            cell.SetValue(newValue);
            //Assert
            cell.ValueStr.Should().Be(newValue);
            cell.IsPlayerSet.Should().BeTrue();
            cell.Index.Should().Be(2);
        }
    }
}
