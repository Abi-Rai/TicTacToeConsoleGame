using TicTacToeBenchmarks.Boards;

namespace TicTacToeBenchmarks.Runner;

public static class GameBoardRunner
{
    public static void PlaySingleBoard(char[] playerInputs)
    {
        SingleBoard singleBoard = new();
        foreach (var playerInput in playerInputs)
        {
            singleBoard.RegisterPlayerMove(playerInput);
            if (singleBoard.CheckWin()) return;
        }
    }
    public static void PlayMultiBoard(char[] playerInputs)
    {
        MultiBoard multiBoard = new();
        foreach (var playerInput in playerInputs)
        {
            multiBoard.RegisterPlayerMove(playerInput);
            if (multiBoard.CheckWin()) return;
        }
    }
    public static void PlayJaggedBoard(char[] playerInputs)
    {
        JaggedBoard jaggedBoard = new();
        foreach (var playerInput in playerInputs)
        {
            jaggedBoard.RegisterPlayerMove(playerInput);
            if (jaggedBoard.CheckWin()) return;
        }
    }
}
