namespace TicTacToe.Game.Board
{
    public struct Cell : IEquatable<Cell>
    {

        public string ValueStr { get; private set; }
        public bool IsPlayerSet;
        public int Index;

        public Cell(string valueStr, int index, bool isPlayerSet)
        {
            ValueStr = valueStr;
            Index = index;
            IsPlayerSet = isPlayerSet;
        }
        public void SetValue(string valueStr)
        {
            IsPlayerSet = true;
            ValueStr = valueStr;
        }

        public bool Equals(Cell other)
        {
            return ValueStr == other.ValueStr;
        }
    }
}
