namespace TicTacToe.Game.Runner.Utils;
public class GameRunnerHelper : GameRunnerBase
{
    public static void Exit()
    {
        Printer.DisplayExitMessage();
    }

    public static string GetComputersMove(string[] availableCells)
    {
        Random rand = new();
        int index = rand.Next(0, availableCells.Length);
        return availableCells[index];
    }

    public static char[] GetValidKeys(char from, char to)
    {
        List<char> validKeys = new();
        while (from <= to)
        {
            validKeys.Add(from++);
        }
        return validKeys.ToArray();
    }

    public static char WaitPlayerValidKeyInput(char from, char to)
    {
        char[] validKeys = GetValidKeys(from, to);
        Printer.DisplayValidKeys(validKeys);
        ConsoleKeyInfo keyPressed = Printer.ReadKey();
        while (InputParser.IsValidKey(keyPressed, validKeys) == false)
        {
            Printer.DisplayInvalidKeyMessage(validKeys);
            keyPressed = Printer.ReadKey();
        }
        return keyPressed.KeyChar;
    }
    public static string WaitPlayerValidStringInput(string[] availableCells)
    {
        var playerInput = Printer.ReadLine();
        while (InputParser.IsValidInput(playerInput, availableCells) == false)
        {
            Printer.DisplayInvalidStringMessage(availableCells);
            playerInput = Printer.ReadLine();
        }
        return playerInput!;
    }
}