namespace TicTacToe.Game.Board
{
    public struct Cell : IEquatable<Cell>
    {

        public string ValueStr { get; private set; }
        public bool IsPlayerSet { get; set; }
        public int Index { get; set; }

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

        public override bool Equals(object? obj)
        {
            return obj is Cell cell && Equals(cell);
        }

        public override int GetHashCode()
        {
            return (ValueStr.GetHashCode() * Index.GetHashCode());
        }

        public static bool operator ==(Cell left, Cell right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Cell left, Cell right)
        {
            return !(left == right);
        }
    }
}
