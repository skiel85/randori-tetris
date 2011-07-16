using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class Board
    {
        private BlockManager _blockManager;
        public int Height { get; set; }
        public int Width { get; set; }
        public Piece CurrentPiece { get; set; }

        public Board()
            : this(10, 1)
        {
        }

        public Board(int height)
            : this(height, 1)
        {
        }

        public Board(int height, int width)
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
                if (IsCurrentPieceInLastValidY())
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
            _blockManager.Add(CurrentPiece.GetBlocks().First());
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
            return _blockManager.CountBlocksAtRow(currentRow) >= this.Width;
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

        public Block AddBlock(int x, int y)
        {
            return _blockManager.AddBlock(x, y);
        }
    }
}
