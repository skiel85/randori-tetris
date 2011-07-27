using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class Board
    {
        private readonly BlockManager _blockManager;
        public int Height { get; set; }
        public int Width { get; set; }
        public Piece CurrentPiece { get; set; }

        public Board()
            : this(1, 10)
        {
        }

        public Board(int height)
            : this(1, height)
        {
        }

        public Board(int width, int height)
        {
            this.Height = height;
            this.Width = width;
            _blockManager = new BlockManager();
        }

        public void AddNewPiece()
        {
            CurrentPiece = new Piece();
        }

        public void Advance()
        {
            if (ExistsCurrentPiece())
            {
                if (IsCurrentPieceInLastValidY() || _blockManager.Collides(CurrentPiece))
                {
                    LeanCurrentPiece();
                }
                else
                {
                    CurrentPiece.MoveDown();
                }
            }
            VerifyAndRemoveLines();
        }

        private bool ExistsCurrentPiece()
        {
            return CurrentPiece != null;
        }

        private void LeanCurrentPiece()
        {
            ExtractCurrentPieceBlocks();
            RemoveCurrentPiece();
        }

        private void ExtractCurrentPieceBlocks()
        {
            CurrentPiece.GetBlocks().Each(b => _blockManager.Add(b));
        }

        private void RemoveCurrentPiece()
        {
            CurrentPiece = null;
        }

        private void VerifyAndRemoveLines()
        {
            AllRows().Each(VerifyAndRemoveLine);
        }

        private IEnumerable<int> AllRows()
        {
            return Enumerable.Range(0, Height);
        }

        private void VerifyAndRemoveLine(int currentRow)
        {
            if (IsFullLine(currentRow))
            {
                _blockManager.RemoveLine(currentRow);
                _blockManager.MoveUpperRowsDown(currentRow);
            }
        }

        private bool IsFullLine(int currentRow)
        {
            return _blockManager.CountBlocksAtRow(currentRow) >= Width;
        }

        private bool IsCurrentPieceInLastValidY()
        {
            return CurrentPiece.Y == GetLastValidY();
        }

        private int GetLastValidY()
        {
            return Height - 1;
        }

        public IEnumerable<Block> GetBlocks()
        {
            return _blockManager.GetBlocks();
        }

        public bool Contains(Block block)
        {
            return _blockManager.Contains(block);
        }

        public Block AddNewBlock(int x, int y)
        {
            return _blockManager.AddBlock(x, y);
        }

        public void AddNewPiece(Block[] blocks)
        {
            CurrentPiece = new Piece(blocks);
        }

        public object GetBlock(int x, int y)
        {
            return _blockManager.GetBlocks().Where(b => b.X == x && b.Y == y).SingleOrDefault();
        }

        public void LoadFromString(string p)
        {
            for (int i = 0; i < p.Length; i++)
            {
                if (p.ElementAt(i) == 'X')
                {
                    _blockManager.AddBlock(i % Width, i / Width);
                }
            }
        }

        public string SaveToString()
        {
            var result = new StringBuilder();
            for (int y = 0; y < Height ; y++)
            {
                for (int x = 0; x < Width ; x++)
                {
                    if (GetBlock(x, y) == null)
                    {
                        result.Append(' ');
                    }
                    else
                    {
                        result.Append('X');
                    }
                }
            }
            return result.ToString();
        }
    }
}
