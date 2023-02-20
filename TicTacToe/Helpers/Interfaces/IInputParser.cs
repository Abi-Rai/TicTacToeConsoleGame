namespace TicTacToe.Helpers.Interfaces
{
    public interface IInputParser
    {
        bool IsValidInput(string? playerInput, string[] availableCells);
        bool IsValidKey(ConsoleKeyInfo inputKey, char[] validKeys);
        bool IsValidMarkerInput(string? playerInput, string playerOneMarker, string playerTwoMarker);
        bool IsValidNameInput(string? playerName, string exisitingNameOne, string exisitingNameTwo);
        char ParseInputToChar(string playerInput);
    }
}