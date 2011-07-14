using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class Board
    {
        private Piece _currentPiece;
        private Block _block;
        public int Height { get; set; }

        public Board()
        {
            Height = 10;
        }

        public Board(int height)
        {
            this.Height = height;
        }

        public Piece CurrentPiece
        {
            get { return _currentPiece; }
            set { _currentPiece = value; }
        }

        public void AddNewPiece()
        {
            _currentPiece = new Piece();
        }

        public void Advance()
        {
            if (_currentPiece != null)
            {
                if (_currentPiece.Height == Height)
                {
                    _block = _currentPiece.GetBlocks().First();
                    _currentPiece = null;
                }
                else
                {
                    _currentPiece.Height += 1;
                }
            }
        }

        public IEnumerable<Block> GetBlocks()
        {
            var blocks = new List<Block>();
            blocks.Add(_block);
            return blocks;
        }

        public bool Contains(Block block)
        {
            return GetBlocks().Any(block.Equals);
        }
    }


}
