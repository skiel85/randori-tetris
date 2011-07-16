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
            if (CurrentPiece != null)
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

        private void LeanCurrentPiece()
        {
            _blocks.Add(CurrentPiece.GetBlocks().First());
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
            var bottomBlocks = _blocks.Where(eachBlock => eachBlock.Y == currentRow).ToArray();
            if (bottomBlocks.Count() >= this.Width)
            {
                foreach (var bottomBlock in bottomBlocks)
                {
                    _blocks.Remove(bottomBlock);
                }
            }
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

        public void AddBlock(int x, int y)
        {
            _blocks.Add(new Block(x, y));
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
            action(en.Current );
        }
    }
}
