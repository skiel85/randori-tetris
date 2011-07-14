using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class Piece
    {
        int height;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        private IList<Block> _block;
        public Piece()
        {
            _block = new List<Block>();
            _block.Add(new Block());
        }

        public IEnumerable<Block> GetBlocks()
        {
            return _block;
        }
    }
}
