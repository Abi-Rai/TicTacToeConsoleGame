﻿using FluentAssertions;
using Moq;
using TicTacToe.Game.Runner;
using TicTacToe.Game.Runner.Utils;
using TicTacToe.Helpers.Interfaces;

namespace TicTacToeTests.RunnerTests
{
    public class GameRunnerTests
    {
        private readonly Mock<IPrinter> _mockPrinter;
        private readonly Mock<IInputParser> _mockInputParser;

        public GameRunnerTests()
        {

            _mockPrinter = new Mock<IPrinter>();
            _mockInputParser = new Mock<IInputParser>();
        }

        [Theory]
        [InlineData('1', '5', new char[] { '1', '2', '3', '4', '5' })]
        [InlineData('1', '2', new char[] { '1', '2' })]
        [InlineData('2', '5', new char[] { '2', '3', '4', '5' })]
        public void GetValidKeys_Should_ReturnValidKeys(char from, char to, char[] expectedArray)
        {
            //Act 
            var result = GameRunnerHelper.GetValidKeys(from, to);
            //Assert
            result.Should().BeEquivalentTo(expectedArray);
        }

        [Theory]
        [InlineData('1', '5', '2', ConsoleKey.D2)]
        [InlineData('1', '2', '1', ConsoleKey.D1)]
        [InlineData('2', '5', '4', ConsoleKey.D4)]
        public void WaitPlayerValidKeyInput_Should_ReturnValidChar_ForValidKeys(char from, char to, char keyChar, ConsoleKey consoleKey)
        {
            //Arrange
            var playerInput = new ConsoleKeyInfo(keyChar, consoleKey, false, false, false);

            char[] validKeys = GameRunnerHelper.GetValidKeys(from, to);

            _mockPrinter.Setup(p => p.ReadKey()).Returns(playerInput);
            _mockPrinter.Setup(p => p.DisplayValidKeys(validKeys)).Verifiable();

            GameRunnerBase.Printer = _mockPrinter.Object;
            _mockInputParser.Setup(ip => ip.IsValidKey(playerInput, validKeys))
                            .Returns(true);
            GameRunnerBase.InputParser = _mockInputParser.Object;

            char expected = keyChar;

            //Act 
            var result = GameRunnerHelper.WaitPlayerValidKeyInput(from, to);
            //Assert
            _mockPrinter.Verify(p => p.DisplayValidKeys(validKeys), Times.Once);
            _mockPrinter.Verify(p => p.ReadKey(), Times.Once);
            result.Should().Be(expected);
        }

        [Fact]
        public void WaitPlayerValidKeyInput_Should_RetryTillValidKeyInput()
        {
            // Arrange
            List<ConsoleKeyInfo> inputKeys = new()
            {
                new ConsoleKeyInfo('1', ConsoleKey.D1, false, false, false),
                new ConsoleKeyInfo('2', ConsoleKey.D2, false, false, false),
                new ConsoleKeyInfo('3', ConsoleKey.D3, false, false, false),
                new ConsoleKeyInfo('4', ConsoleKey.D4, false, false, false)
            };

            char[] validKeys = GameRunnerHelper.GetValidKeys('4', '6');

            var _mockPrinter = new Mock<IPrinter>();
            _mockPrinter.SetupSequence(p => p.ReadKey())
            .Returns(inputKeys[0])
            .Returns(inputKeys[1])
            .Returns(inputKeys[2])
            .Returns(inputKeys[3]);
            _mockPrinter.Setup(p => p.DisplayValidKeys(validKeys)).Verifiable();
            _mockPrinter.Setup(p => p.DisplayInvalidKeyMessage(validKeys)).Verifiable();

            GameRunnerBase.Printer = _mockPrinter.Object;
            char expected = '4';
            _mockInputParser.Setup(ip => ip.IsValidKey(It.IsAny<ConsoleKeyInfo>(), validKeys))
                            .Returns<ConsoleKeyInfo, char[]>((key, keys) =>
                            {
                                return key.KeyChar == expected;
                            });
            GameRunnerBase.InputParser = _mockInputParser.Object;

            // Act 
            var result = GameRunnerHelper.WaitPlayerValidKeyInput('4', '6');

            // Assert
            _mockPrinter.Verify(p => p.DisplayValidKeys(validKeys), Times.Once);
            _mockPrinter.Verify(p => p.DisplayInvalidKeyMessage(validKeys), Times.Exactly(3));
            _mockPrinter.Verify(p => p.ReadKey(), Times.Exactly(4));
            _mockInputParser.Verify(ip => ip.IsValidKey(It.IsAny<ConsoleKeyInfo>(), validKeys), Times.Exactly(4));
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("1", new string[] { "1", "2", "3" })]
        [InlineData("5", new string[] { "4", "5", "6" })]
        [InlineData("8", new string[] { "7", "8", "9" })]
        public void WaitPlayerValidStringInput_Should_ReturnValidString_ForValidInputs(string playerInput, string[] availableCells)
        {
            //Arrange
            _mockPrinter.Setup(p => p.ReadLine()).Returns(playerInput);

            GameRunnerBase.Printer = _mockPrinter.Object;
            _mockInputParser.Setup(ip => ip.IsValidInput(It.IsAny<string>(), availableCells))
                            .Returns((string s, string[] cells) =>
                            {
                                return cells.Contains(s);
                            });

            GameRunnerBase.InputParser = _mockInputParser.Object;
            //Act 
            var result = GameRunnerHelper.WaitPlayerValidStringInput(availableCells);

            //Assert
            _mockPrinter.Verify(p => p.ReadLine(), Times.Once);
            availableCells.Should().Contain(result);
        }

        [Fact]
        public void WaitPlayerValidStringInput_ShouldLoopUntil_ValidInput()
        {
            //Arrange

            _mockPrinter.SetupSequence(p => p.ReadLine())
                        .Returns("A")
                        .Returns("&^$")
                        .Returns("Two")
                        .Returns(string.Empty)
                        .Returns("4");

            GameRunnerBase.Printer = _mockPrinter.Object;
            string[] availableCells = new string[] { "2", "3", "4", "5" };
            _mockInputParser.Setup(ip => ip.IsValidInput(It.IsAny<string>(), availableCells))
                            .Returns((string s, string[] cells) =>
                            {
                                return cells.Contains(s);
                            });

            GameRunnerBase.InputParser = _mockInputParser.Object;
            //Act 
            var result = GameRunnerHelper.WaitPlayerValidStringInput(availableCells);

            //Assert
            _mockPrinter.Verify(p => p.DisplayInvalidStringMessage(availableCells), Times.Exactly(4));
            _mockInputParser.Verify(p => p.IsValidInput(It.IsAny<string>(), availableCells), Times.Exactly(5));
            _mockPrinter.Verify(p => p.ReadLine(), Times.Exactly(5));
            availableCells.Should().Contain(result);
            result.Should().Be("4");
        }

        [Theory]
        [InlineData("1", "2", "3")]
        [InlineData("1", "2", "3", "4", "5", "6")]
        [InlineData("4", "5", "6", "7", "8", "9")]
        public void GetComputersMove_Should_ReturnAValidString(params string[] availableCells)
        {
            //Act 
            var result = GameRunnerHelper.GetComputersMove(availableCells);
            //Assert
            availableCells.Should().Contain(result);
        }
    }
}
