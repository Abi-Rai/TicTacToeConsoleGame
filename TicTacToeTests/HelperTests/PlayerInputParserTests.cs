using FluentAssertions;
using TicTacToe.Helpers;

namespace TicTacToeTests.HelperTests;

public class PlayerInputParserTests
{
    private readonly InputParser _inputParser;
    public PlayerInputParserTests()
    {
        _inputParser = new InputParser();
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("6  ")]
    [InlineData("   8  ")]
    public void ValidInputs_IsValidInput_ShouldBeTrue(string playerInput)
    {
        //Arrange
        string[] availableCells = new string[] { "1", "2", "8", "3", "6" };
        //Act 
        bool result = _inputParser.IsValidInput(playerInput, availableCells);
        //Assert
        result.Should().Be(true);
    }

    [Theory]
    [InlineData("A")]
    [InlineData("DF ")]
    [InlineData("3X")]
    [InlineData(".5")]
    [InlineData("7%")]
    [InlineData("10")]
    [InlineData("-1")]
    [InlineData("3")]
    public void InvalidInputs_IsValidInput_ShouldBeFalse(string playerInput)
    {
        string[] availableCells = new string[] { "1", "2" };
        //Act 
        bool result = _inputParser.IsValidInput(playerInput, availableCells);
        //Assert
        result.Should().Be(false);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("6  ")]
    [InlineData("   9  ")]
    public void GivenAValidInputString_ParseInputToChar_ShouldReturnAChar(string playerInput)
    {
        //Arrange
        char expected = char.Parse(playerInput.Trim());

        //Act 
        char result = _inputParser.ParseInputToChar(playerInput);
        //Assert
        result.Should().Be(expected);
    }
    [Theory]
    [InlineData("DF ")]
    [InlineData("3X")]
    [InlineData(".5")]
    [InlineData("7%")]
    [InlineData("10")]
    [InlineData("50")]
    [InlineData("90")]
    [InlineData("-1")]
    public void GivenAWrongFormatInputString_ThenThrowInvalidCastException(string invalidInput)
    {
        //Act 
        Action act = () => _inputParser.ParseInputToChar(invalidInput);
        //Assert
        act.Should().Throw<InvalidCastException>();

    }
    [Theory]
    [InlineData("A")]
    [InlineData("X")]
    [InlineData("0")]
    [InlineData("-")]
    public void GivenAnOutOfBoundsString_ThenThrowOutOfRangeException(string invalidInput)
    {
        //Act 
        Action act = () => _inputParser.ParseInputToChar(invalidInput);
        //Assert
        act.Should().Throw<ArgumentOutOfRangeException>();

    }

    [Fact]
    public void GivenAValidKey_IsValidKey_ShouldBeTrue()
    {
        //Arrange
        List<ConsoleKeyInfo> inputKeys = new()
        {
            new ConsoleKeyInfo('1',ConsoleKey.D1,false,false,false),
            new ConsoleKeyInfo('2', ConsoleKey.D2, false, false, false),
            new ConsoleKeyInfo('3', ConsoleKey.D3, false, false, false),
            new ConsoleKeyInfo('4', ConsoleKey.D4, false, false, false)
        };
        char[] validKeys = new char[] { '1', '2', '3', '4' };
        foreach (var inputKey in inputKeys)
        {
            //Act 
            var result = _inputParser.IsValidKey(inputKey, validKeys);

            //Assert
            result.Should().BeTrue();
        }
    }
    [Fact]
    public void GivenAnInValidKey_IsValidKey_ShouldBeFalse()
    {
        //Arrange
        List<ConsoleKeyInfo> inputKeys = new()
        {
            new ConsoleKeyInfo('9',ConsoleKey.D9,false,false,false),
            new ConsoleKeyInfo('8', ConsoleKey.D8, false, false, false),
            new ConsoleKeyInfo('7', ConsoleKey.D7, false, false, false),
            new ConsoleKeyInfo('6', ConsoleKey.D6, false, false, false)
        };
        char[] validKeys = new char[] { '1', '2', '3', '4' };
        foreach (var inputKey in inputKeys)
        {
            //Act 
            var result = _inputParser.IsValidKey(inputKey, validKeys);

            //Assert
            result.Should().BeFalse();
        }
    }

    [Theory]
    [InlineData("T")]
    [InlineData("Y")]
    [InlineData("D")]
    [InlineData("Z")]
    [InlineData("8")]
    public void GivenAValidInput_IsValidMarkerInput_ShouldBeTrue(string validPlayerInput)
    {
        //Arrange
        string playerOneMarker = "X";
        string playerTwoMarker = "O";
        //Act 
        var result = _inputParser.IsValidMarkerInput(validPlayerInput,playerOneMarker,playerTwoMarker);

        //Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("  ")]
    [InlineData(" ***12 ")]
    [InlineData("")]
    [InlineData("X")]
    [InlineData("ZD")]
    public void GivenAnInValidInput_IsValidMarkerInput_ShouldBeFalse(string validPlayerInput)
    {
        //Arrange
        string playerOneMarker = "X";
        string playerTwoMarker = "O";
        //Act 
        var result = _inputParser.IsValidMarkerInput(validPlayerInput, playerOneMarker, playerTwoMarker);

        //Assert
        result.Should().BeFalse();
    }
}

