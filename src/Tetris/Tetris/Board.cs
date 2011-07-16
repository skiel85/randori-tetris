using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class Board
    {
        private readonly List<Block> _blocks = new List<Block>();
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
            _blocks.Add(CurrentPiece.GetBlocks().First());
        }

        private Piece RemoveCurrentPiece()
        {
            return CurrentPiece = null;
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
                RemoveLine(currentRow);
                MoveUpperRowsDown(currentRow);
            }
        }

        private bool IsFullLine(int currentRow)
        {
            return CountBlocksAtRow(currentRow) >= this.Width;
        }

        private int CountBlocksAtRow(int currentRow)
        {
            return GetBlocksAtRow(currentRow).Count();
        }

        private IEnumerable<Block> GetBlocksAtRow(int currentRow)
        {
            return _blocks.Where(IsAtRow(currentRow));
        }

        private static Func<Block, bool> IsAtRow(int currentRow)
        {
            return eachBlock => eachBlock.Y == currentRow;
        }

        private void RemoveLine(int currentRow)
        {
            _blocks.Where(eachBlock => eachBlock.Y == currentRow).ToArray().Each(RemoveBlock);
        }

        private void MoveUpperRowsDown(int currentRow)
        {
            _blocks.Where(IsUpperTo(currentRow)).Each(MoveBlockDown());
        }

        private static Action<Block> MoveBlockDown()
        {
            return b => b.Y++;
        }

        private static Func<Block, bool> IsUpperTo(int currentRow)
        {
            return eachBlock => eachBlock.Y < currentRow;
        }

        private void RemoveBlock(Block b)
        {
            _blocks.Remove(b);
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
            return _blocks.AsEnumerable();
        }

        public bool Contains(Block block)
        {
            return GetBlocks().Any(block.Equals);
        }

        public Block AddBlock(int x, int y)
        {
            var block = new Block(x, y);
            _blocks.Add(block);
            return block;
        }
    }

    public static class EachExtension
    {
        public static void Each<T>(this IEnumerable<T> enumberable, Action<T> action)
        {
            foreach (var item in enumberable)
            {
                action(item);
            }
        }

        public static void Second<T>(this IEnumerable<T> enumberable, Action<T> action)
        {
            var en = enumberable.GetEnumerator();
            en.MoveNext();
            action(en.Current);
        }
    }
}
