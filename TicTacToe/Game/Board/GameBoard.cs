namespace TicTacToe.Game.Board
{
    public class GameBoard
    {
        private Cell[] _board;
        public int BoardSize { get; private set; }
        public RowSizes RowSize { get; private set; }
        public int RowLength { get; private set; }
        public Cell[] Board { get => _board; set => _board = value; }
        public Stack<string> MoveHistory { get; private set; }

        public int TotalMoves;

        public GameBoard(RowSizes numberOfCells = RowSizes.Three)
        {
            RowSize = numberOfCells;
            BoardSize = (int)numberOfCells;
            RowLength = (int)Math.Sqrt(BoardSize);
            TotalMoves = 0;
            MoveHistory = new Stack<string>();
            _board = NewGame();
        }


        public Cell[] NewGame()
        {
            var emptyBoard = new Cell[BoardSize];
            int cellValueNum = 1;
            for (int i = 0; i < emptyBoard.Length; i++)
            {
                emptyBoard[i] = new Cell(cellValueNum.ToString(), i, false);
                cellValueNum++;
            }
            return emptyBoard;
        }

        /// <returns><see langword="true"/> If there the passed <paramref name="player"/> made the winning move</returns>
        public bool MakeMove(Player player, string playerInput)
        {
            var cell = _board.First(c => c.ValueStr == playerInput);
            LogMove(player, cell);
            cell.SetValue(player.Marker);
            _board[cell.Index] = cell;

            if (TotalMoves > RowLength)
            {
                return CheckWin();
            }
            return false;
        }
        private bool CheckWin()
        {
            if (CheckRows() ||
                CheckColumns() ||
                CheckDiagonals()
                ) return true;
            return false;
        }
        private void LogMove(Player player, Cell cell)
        {
            string moveLog = $"player:{player.Name} set their marker:{player.Marker} on cell:{cell.ValueStr}";
            MoveHistory.Push(moveLog);
            TotalMoves++;
        }
        private bool CheckDiagonals()
        {
            bool match1 = true;
            string marker1 = _board[0].ValueStr;

            for (int i = 1; i < RowLength; i++)
            {
                if (_board[i * RowLength + i].ValueStr != marker1)
                {
                    match1 = false;
                    break;
                }
            }

            if (match1 && !string.IsNullOrEmpty(marker1))
            {
                return true;
            }

            bool match2 = true;
            string marker2 = _board[RowLength - 1].ValueStr;

            for (int i = 1; i < RowLength; i++)
            {
                if (_board[(i + 1) * (RowLength - 1)].ValueStr != marker2)
                {
                    match2 = false;
                    break;
                }
            }

            if (match2 && !string.IsNullOrEmpty(marker2))
            {
                return true;
            }

            return false;
        }

        private bool CheckColumns()
        {
            for (int i = 0; i < RowLength; i++)
            {
                bool match = true;
                string marker = _board[i].ValueStr;

                for (int j = 1; j < RowLength; j++)
                {
                    if (_board[j * RowLength + i].ValueStr != marker)
                    {
                        match = false;
                        break;
                    }
                }

                if (match && !string.IsNullOrEmpty(marker))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckRows()
        {
            for (int i = 0; i < RowLength; i++)
            {
                bool match = true;
                string marker = _board[i * RowLength].ValueStr;

                for (int j = 1; j < RowLength; j++)
                {
                    if (_board[i * RowLength + j].ValueStr != marker)
                    {
                        match = false;
                        break;
                    }
                }

                if (match && !string.IsNullOrEmpty(marker))
                {
                    return true;
                }
            }

            return false;
        }

        public enum RowSizes
        {
            Two = 4,
            Three = 9,
            Four = 16,
            Five = 25
        }
    }
}
