using TicTacToe.Helpers;

namespace TicTacToe.Runner.Utils;
internal static class GameRunnerHelpers
{
    internal static void Exit()
    {
        StaticPrinter.DisplayExitMessage();
    }

    internal static string GetComputersMove(string[] availableCells)
    {
        Random rand = new();
        int index = rand.Next(0, availableCells.Length);
        return availableCells[index];
    }

    internal static char[] GetValidKeys(char from, char to)
    {
        List<char> validKeys = new();
        while (from <= to)
        {
            validKeys.Add(from++);
        }
        return validKeys.ToArray();
    }

    internal static char WaitPlayerValidKeyInput(char from, char to)
    {
        char[] validKeys = GetValidKeys(from, to);
        StaticPrinter.DisplayValidKeys(validKeys);
        ConsoleKeyInfo keyPressed = StaticPrinter.ReadKey();
        while (PlayerInputParser.IsValidKey(keyPressed, validKeys) == false)
        {
            StaticPrinter.DisplayInvalidKeyMessage(validKeys);
            keyPressed = StaticPrinter.ReadKey();
        }
        return keyPressed.KeyChar;
    }
    internal static string WaitPlayerValidStringInput(string[] availableCells)
    {
        var playerInput = StaticPrinter.ReadLine();
        while (PlayerInputParser.IsValidInput(playerInput, availableCells) == false)
        {
            StaticPrinter.DisplayInvalidStringMessage(availableCells);
            playerInput = StaticPrinter.ReadLine();
        }
        return playerInput!;
    }
}