using FluentAssertions;
using TicTacToe.Helpers;

namespace TicTacToeTests.HelperTests;

public class PrinterTests
{
    [Fact]
    public void WriteLineWithText_ShouldWriteToConsoleWithNewLine()
    {
        //Arrage
        using StringWriter stringWriter = new();
        Console.SetOut(stringWriter);
        string textToDisplay = "Hello John";
        string expected = string.Format("Hello John{0}", Environment.NewLine);

        //Act
        StaticPrinter.WriteLine(textToDisplay);

        //Assert
        expected.Should<string>().Be(stringWriter.ToString());
    }
    [Fact]
    public void WriteLineWithoutText_ShouldWriteToConsoleWithNewLine()
    {
        //Arrage
        using StringWriter stringWriter = new();
        Console.SetOut(stringWriter);
        string expected = string.Format(Environment.NewLine);

        //Act
        StaticPrinter.WriteLine();

        //Assert
        expected.Should<string>().Be(stringWriter.ToString());
    }
    [Fact]
    public void Write_ShouldWriteToConsole()
    {
        //Arrange
        using StringWriter stringWriter = new();
        Console.SetOut(stringWriter);
        string textToDisplay = "single line";
        string expected = string.Format("single line");

        //Act
        StaticPrinter.Write(textToDisplay);

        //Assert
        expected.Should<string>().Be(stringWriter.ToString());
    }
    [Fact]
    public void ReadLineValid_ShouldReturnInputFromConsole()
    {
        //Arrange
        string testInput = "test";
        StringReader stringReader = new(testInput);
        Console.SetIn(stringReader);

        //Act
        string? result = StaticPrinter.ReadLine();

        //Assert
        result.Should<string>().Be(testInput);
    }
    [Fact]
    public void ReadLineEmpty_ShouldReturnNull()
    {
        //Arrange
        string testInput = "";
        StringReader stringReader = new(testInput);
        Console.SetIn(stringReader);

        //Act
        string? result = StaticPrinter.ReadLine();

        //Assert
        result.Should<string>().BeNull();
    }
}