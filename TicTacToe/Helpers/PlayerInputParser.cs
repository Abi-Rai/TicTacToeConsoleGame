using TicTacToe.CustomException;

namespace TicTacToe.Helpers
{
    public static class PlayerInputParser
    {
        public static bool IsValidNameInput(string? playerName, string exisitingNameOne, string exisitingNameTwo)
        {
            if (string.IsNullOrWhiteSpace(playerName)) return false;
            playerName = playerName.Trim();
            return (playerName != exisitingNameOne && playerName != exisitingNameTwo);
        }
        public static bool IsValidInput(string? playerInput, string[] availableCells)
        {
            if (string.IsNullOrWhiteSpace(playerInput)) return false;
            playerInput = playerInput.Trim();
            if (playerInput.ToLower() == "exit")
            {
                throw new UserRequestExitException("Player wishes to exit game");
            }

            return availableCells.Contains(playerInput);
        }

        public static char ParseInputToChar(string playerInput)
        {
            if (char.TryParse(playerInput.Trim(), out char output))
            {
                if (output > '0' && output <= '9') return output;
                else throw new ArgumentOutOfRangeException(playerInput);
            }
            else
            {
                throw new InvalidCastException();
            }
        }
        public static bool IsValidKey(ConsoleKeyInfo inputKey, char[] validKeys)
        {
            return validKeys.Contains(inputKey.KeyChar);
        }

        public static bool IsValidMarkerInput(string? playerInput, string playerOneMarker, string playerTwoMarker)
        {
            if (string.IsNullOrWhiteSpace(playerInput) || playerInput.Length > 1) return false;
            playerInput = playerInput.Trim();
            return (playerInput != playerOneMarker && playerInput != playerTwoMarker);
        }
    }
}