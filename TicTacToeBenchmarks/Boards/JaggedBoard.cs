namespace TicTacToeBenchmarks.Boards;

public class JaggedBoard
{
    public char[][] Board { get; } = new char[][]
    {
        new char[] {'1', '2', '3'},
        new char[] {'4', '5', '6'},
        new char[] {'7', '8', '9'}
    };
    private readonly Dictionary<char, (int X, int Y)> _pointSelector = new();

    private bool _player1 = true;
    private int _moveCount = 0;
    public JaggedBoard()
    {
        CreatePointSelector();
    }
    private void CreatePointSelector()
    {
        int pointIndex = 1;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                _pointSelector.Add((char)('0' + pointIndex++), (x, y));
            }
        }
    }
    public void RegisterPlayerMove(char move)
    {
        var pointIndex = _pointSelector[move];
        Board[pointIndex.X][pointIndex.Y] = _player1 ? 'X' : 'O';
        _moveCount++;
        _player1 = !_player1;
    }
    public bool CheckWin()
    {
        if (Board[0][0] == Board[0][1] && Board[0][1] == Board[0][2] || // Check row 1 (cells 1, 2, 3)
            Board[1][0] == Board[1][1] && Board[1][1] == Board[1][2] || // Check row 2 (cells 4, 5, 6)
            Board[2][0] == Board[2][1] && Board[2][1] == Board[2][2] || // Check row 3 (cells 7, 8, 9)
            Board[0][0] == Board[1][0] && Board[1][0] == Board[2][0] || // Check column 1 (cells 1, 4, 7)
            Board[0][1] == Board[1][1] && Board[1][1] == Board[2][1] || // Check column 2 (cells 2, 5, 8)
            Board[0][2] == Board[1][2] && Board[1][2] == Board[2][2] || // Check column 3 (cells 3, 6, 9)
            Board[0][0] == Board[1][1] && Board[1][1] == Board[2][2] || // Check diagonal 1 (cells 1, 5, 9)
            Board[0][2] == Board[1][1] && Board[1][1] == Board[2][0] ||   // Check diagonal 2 (cells 3, 5, 7)
            CheckDraw()) return true;
        return false;
    }
    private bool CheckDraw() => _moveCount >= 9;
}
