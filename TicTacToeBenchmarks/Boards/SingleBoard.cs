namespace TicTacToeBenchmarks.Boards;

public class SingleBoard
{
    public char[] Board { get; } = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    private readonly Dictionary<char, int> _pointSelector = new(); // Not really needed, but added for the sake of benchmarking at equal loads.

    private bool _player1 = true;
    private int _moveCount = 0;
    public SingleBoard()
    {
        CreatePointSelector();
    }
    private void CreatePointSelector()
    {
        int pointIndex = 1;
        for (int i = 0; i < 9; i++)
        {
            _pointSelector.Add((char)('0' + pointIndex++), i);
        }
    }
    public void RegisterPlayerMove(char move)
    {
        var point = _pointSelector[move];
        Board[point] = _player1 ? 'X' : 'O';
        _moveCount++;
        _player1 = !_player1;
    }
    public bool CheckWin()
    {
        if (Board[0] == Board[1] && Board[1] == Board[2] || // Check row 1 (cells 1, 2, 3)
            Board[3] == Board[4] && Board[4] == Board[5] || // Check row 2 (cells 4, 5, 6)
            Board[6] == Board[7] && Board[7] == Board[8] || // Check row 3 (cells 7, 8, 9)
            Board[0] == Board[3] && Board[3] == Board[6] || // Check column 1 (cells 1, 4, 7)
            Board[1] == Board[4] && Board[4] == Board[7] || // Check column 2 (cells 2, 5, 8)
            Board[2] == Board[5] && Board[5] == Board[8] || // Check column 3 (cells 3, 6, 9)
            Board[0] == Board[4] && Board[4] == Board[8] || // Check diagonal 1 (cells 1, 5, 9)
            Board[2] == Board[4] && Board[4] == Board[6] || // Check diagonal 2 (cells 3, 5, 7)
            CheckDraw()
            ) return true;
        return false;
    }
    private bool CheckDraw() => _moveCount >= 9;

}
