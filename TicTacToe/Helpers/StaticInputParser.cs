using System.Runtime.CompilerServices;
using TicTacToe.Helpers.Interfaces;

[assembly: InternalsVisibleTo("TicTacToeTests")]
namespace TicTacToe.Helpers
{
    public static class StaticInputParser
    {
        internal static IInputParser _inputParser = new InputParser();
        public static bool IsValidNameInput(string? playerName, string exisitingNameOne, string exisitingNameTwo) => _inputParser.IsValidNameInput(playerName, exisitingNameOne, exisitingNameTwo);
        public static bool IsValidInput(string? playerInput, string[] availableCells) => _inputParser.IsValidInput(playerInput, availableCells);

        public static char ParseInputToChar(string playerInput) => _inputParser.ParseInputToChar(playerInput);
        public static bool IsValidKey(ConsoleKeyInfo inputKey, char[] validKeys) => _inputParser.IsValidKey(inputKey, validKeys);

        public static bool IsValidMarkerInput(string? playerInput, string playerOneMarker, string playerTwoMarker) => _inputParser.IsValidMarkerInput(playerInput, playerOneMarker, playerTwoMarker);
    }
}