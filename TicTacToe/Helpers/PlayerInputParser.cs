using TicTacToe.Helpers.Interfaces;

namespace TicTacToe.Helpers
{
    public static class PlayerInputParser
    {
        public static IInputParser _inputParser = new InputParser();
        public static bool IsValidNameInput(string? playerName, string exisitingNameOne, string exisitingNameTwo)
        {
            return _inputParser.IsValidNameInput(playerName, exisitingNameOne, exisitingNameTwo);
        }
        public static bool IsValidInput(string? playerInput, string[] availableCells)
        {
            return _inputParser.IsValidInput(playerInput, availableCells);
        }

        public static char ParseInputToChar(string playerInput)
        {
            return _inputParser.ParseInputToChar(playerInput);
        }
        public static bool IsValidKey(ConsoleKeyInfo inputKey, char[] validKeys)
        {
            return _inputParser.IsValidKey(inputKey, validKeys);
        }

        public static bool IsValidMarkerInput(string? playerInput, string playerOneMarker, string playerTwoMarker)
        {
            return _inputParser.IsValidMarkerInput(playerInput, playerOneMarker, playerTwoMarker);
        }
    }
}